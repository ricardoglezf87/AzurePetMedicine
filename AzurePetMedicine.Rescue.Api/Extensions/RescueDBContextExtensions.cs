using AzurePetMedicine.Rescue.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Rescue.Api.Extensions
{
    public static class RescueDbContextExtensions
    {
        public static void AddRescueDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RescueDbContext>(options =>
                options.UseSqlite("Data Source=Rescues.db"));


        public static void EnsureRescueDatabaseCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RescueDbContext>();            
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}
