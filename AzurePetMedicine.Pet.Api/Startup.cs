using AzurePetMedicine.Pet.Api.ApplicationServices;
using AzurePetMedicine.Pet.Api.Extensions;
using AzurePetMedicine.Pet.Api.Infrastructure;
using AzurePetMedicine.Pet.Domain.Repositories;

namespace AzurePetMedicine.Pet.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPetDBContext(Configuration);
            services.AddScoped < IPetRepository, PetRepository >();
            services.AddScoped<PetApplicationServices>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
                });
            }
            app.EnsurePetDatabaseCreated();
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
