using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Hospital.Api.ApplicationServices;
using AzurePetMedicine.Hospital.Api.Extensions;
using AzurePetMedicine.Hospital.Api.Infrastructure;
using AzurePetMedicine.Hospital.Api.IntegrationEvents;
using AzurePetMedicine.Hospital.Domain.Repositories;
using AzurePetMedicine.Hospital.Infraestructure.Repositories;
using AzurePetMedicine.ServiceBus.Infrastructure;

namespace AzurePetMedicine.Hospital.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddHospitalDbContext(Configuration);
            services.AddSingleton<IPatientAggregateStore, PatientAggregateStore>();
            services.AddScoped<PatientApplicationServices>();
            services.AddHostedService<PetTransferredToHospitalIntegrationEventHandle>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            if (Environment.IsDevelopment())
            {
                services.AddHttpServiceBusSimulator(Configuration["serverurl"] ??
                    throw new Exception("Server URL is not configured"));
            }

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });                
            }
            app.EnsureHospitalDatabaseCreated();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            } );
        }
    }
}
