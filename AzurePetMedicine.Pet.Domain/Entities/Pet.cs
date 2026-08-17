using Microsoft.EntityFrameworkCore;
using AzurePetMedicine.Pet.Domain.Events;

namespace AzurePetMedicine.Pet.Domain.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty;
        public int Age { get; set; }

        public Pet(Guid id)
        {
            Id = id;
        }

        public void FlagForAdoption()
        {
            Validate();
            DomainEvents.PetFlaggedForAdoption.Publish(new PetFlaggedForAdoption(Id, Name, Kind, Age));
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(Kind))
                throw new ArgumentException("Kind cannot be empty.");
            if (Age < 0)
                throw new ArgumentException("Age cannot be negative.");
        }
    }
}