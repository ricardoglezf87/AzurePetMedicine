using AzurePetMedicine.Pet.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Pet.Api.Extensions
{
    public static class PetDbContextExtensions
    {
        public static void AddPetDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<PetDbContext>(options =>
                options.UseSqlite("Data Source=Pets.db"));


        public static void EnsurePetDatabaseCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PetDbContext>();            
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}
