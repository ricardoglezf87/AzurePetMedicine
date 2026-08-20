using AzurePetMedicine.Rescue.Api.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AzurePetMedicine.Rescue.Api.Infrastructure
{
    public class RescueDbContext : DbContext
    {
        public DbSet<Domain.Entities.Rescue> Rescues { get; set; }

        public DbSet<Domain.Entities.RescuedAnimal> RescuedAnimals { get; set; }

        public DbSet<PetFlaggedForAdoptionIntegrationEvent> RescueAnimalsMetadata { get; set; }

        public RescueDbContext(DbContextOptions<RescueDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.Rescue>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            });

            modelBuilder.Entity<Domain.Entities.RescuedAnimal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AdopterId).IsRequired();
                entity.Property(e => e.RescuedAnimalAdoptionStatus).IsRequired();
            });
        }
    }
}
