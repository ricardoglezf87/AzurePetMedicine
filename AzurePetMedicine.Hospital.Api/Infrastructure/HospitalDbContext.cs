using AzurePetMedicine.Hospital.Api.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Hospital.Api.Infrastructure
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Domain.Entities.Patient> Patients { get; set; }

        public DbSet<PetTransferredToHospitalIntegrationEvent> PatientMetadata { get; set; }


        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.OwnsOne(e => e.BloodType);                
                entity.OwnsOne(e => e.Weight);
                entity.Property(e => e.Status);
            });
        }
    }
}
