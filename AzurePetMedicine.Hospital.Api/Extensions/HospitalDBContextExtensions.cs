using AzurePetMedicine.Hospital.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Hospital.Api.Extensions
{
    public static class HospitalDBContextExtensions
    {
        public static void AddHospitalDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlite("Data Source=../Hospitals.db"));


        public static void EnsureHospitalDatabaseCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();            
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}
