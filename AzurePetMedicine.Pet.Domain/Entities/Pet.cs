using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Pet.Domain.Events;
using AzurePetMedicine.Pets.Domain.ValueObjects;

namespace AzurePetMedicine.Pet.Domain.Entities
{
    public class Pet: IEntity, IMappableEntity
    {   
        public Guid Id { get; set; }
        public PetName Name { get; set; } 
        public PetKind Kind { get; set; } 
        public PetAge Age { get; set; }

        public Pet() 
        { 
        }

        public Pet(Guid id)
        {
            Id = id;
        }

        public void MapFromEntity(object entity)
        {
            if (entity is not Pet pet)
                throw new ArgumentException("Invalid Entity type.");
            Id = pet.Id;
            Name = pet.Name;
            Kind = pet.Kind;
            Age = pet.Age;
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