using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Rescue.Api.ApplicationServices;
using AzurePetMedicine.Rescue.Api.Extensions;
using AzurePetMedicine.Rescue.Api.Infrastructure;
using AzurePetMedicine.Rescue.Api.IntegrationEvents;
using AzurePetMedicine.ServiceBus.Infrastructure;

namespace AzurePetMedicine.Rescue.Api
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
            services.AddRescueDbContext(Configuration);
            services.AddScoped<IGenericRepository<Domain.Entities.Adopter>, RescueRepository>();
            services.AddScoped<IGenericRepository<Domain.Entities.RescuedAnimal>, RescuedAnimalRepository>();
            services.AddScoped<AdopterApplicationServices>();
            services.AddHostedService<PetFlaggerForAdoptionIntegrationEventHandler>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            if (Environment.IsDevelopment())
            {
                services.AddHttpServiceBusSimulator(Configuration["serverurl"] ?? 
                    throw  new Exception("Server URL is not configured"));
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
            app.EnsureRescueDatabaseCreated();
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
