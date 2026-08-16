using Microsoft.EntityFrameworkCore;
using PetsService.Events;

namespace PetsService.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string kind { get; set; } = string.Empty;
        public int Age { get; set; }

        public void FlagForAdoption()
        {
            Validate();
            DomainEvents.PetFlaggedForAdoption.Publish(new PetFlaggedForAdoption(Id, Name, kind, Age));
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Kind cannot be empty.");
            if (Age < 0)
                throw new ArgumentException("Age cannot be negative.");
        }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Pet> Pets => Set<Pet>();
    }
}