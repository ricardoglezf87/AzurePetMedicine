using AzurePetMedicine.Rescue.Api.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AzurePetMedicine.Rescue.Api.Infrastructure
{
    public class RescueDbContext : DbContext
    {
        public DbSet<Domain.Entities.Adopter> Adopters { get; set; }

        public DbSet<Domain.Entities.RescuedAnimal> RescuedAnimals { get; set; }

        public DbSet<PetFlaggedForAdoptionIntegrationEvent> RescueAnimalsMetadata { get; set; }

        public RescueDbContext(DbContextOptions<RescueDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.Adopter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.OwnsOne(e => e.Name);
                entity.OwnsOne(e => e.PhoneNumber);
            });

            modelBuilder.Entity<Domain.Entities.RescuedAnimal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AdopterId);
                entity.Property(e => e.RescuedAnimalAdoptionStatus);
            });
        }
    }
}
