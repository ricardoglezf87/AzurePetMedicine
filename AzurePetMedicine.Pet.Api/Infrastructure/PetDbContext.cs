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
                entity.OwnsOne(e => e.Name);
                entity.OwnsOne(e => e.Kind);
                entity.OwnsOne(e => e.Age);
            });
        }
    }
}
