using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Pet.Api.ApplicationServices;
using AzurePetMedicine.Pet.Api.Extensions;
using AzurePetMedicine.Pet.Api.Infrastructure;
using AzurePetMedicine.ServiceBus.Infrastructure;

namespace AzurePetMedicine.Pet.Api
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
            services.AddPetDbContext(Configuration);
            services.AddScoped<IGenericRepository<Domain.Entities.Pet>, PetRepository>();
            services.AddScoped<PetApplicationServices>();
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
            app.EnsurePetDatabaseCreated();
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
