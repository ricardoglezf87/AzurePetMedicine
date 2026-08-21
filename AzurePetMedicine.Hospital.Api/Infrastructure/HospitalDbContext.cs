using AzurePetMedicine.Hospital.Api.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Hospital.Api.Infrastructure
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<PetTransferredToHospitalIntegrationEvent> PatientMetadata { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }       
    }
}
