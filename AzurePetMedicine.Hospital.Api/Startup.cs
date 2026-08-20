using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Hospital.Api.ApplicationServices;
using AzurePetMedicine.Hospital.Api.Extensions;
using AzurePetMedicine.Hospital.Api.Infrastructure;
using AzurePetMedicine.Hospital.Api.IntegrationEvents;
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
            services.AddHospitalDbContext(Configuration);
            services.AddScoped<IGenericRepository<Domain.Entities.Patient>, PatientRepository>();
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
                endpoints.MapControllers();
            } );
        }
    }
}
