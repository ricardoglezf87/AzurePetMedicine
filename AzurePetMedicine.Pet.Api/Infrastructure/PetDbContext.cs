using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AzurePetMedicine.Pet.Api.Infrastructure
{
    public class PetDbContext : DbContext
    {
        public DbSet<Domain.Entities.Pet> Pets { get; set; }

        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.Pet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Kind).IsRequired();
                entity.Property(e => e.Age).IsRequired();
            });
        }
    }
}
