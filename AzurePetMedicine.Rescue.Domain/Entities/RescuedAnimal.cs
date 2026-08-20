using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Rescue.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AzurePetMedicine.Rescue.Domain.Entities
{
    public class RescuedAnimal : IEntity, IMappableEntity
    {
        public Guid Id { get; set; }
        public Guid AdopterId { get; set; }
        public RescuedAnimalAdoptionStatus RescuedAnimalAdoptionStatus { get; set; } = RescuedAnimalAdoptionStatus.None;
  
        public RescuedAnimal()
        {
        }
        public RescuedAnimal(Guid id)
        {
            Id = id;
        }

        public void MapFromEntity(object entity)
        {
            if (entity is not RescuedAnimal rescuedAnimal)
                throw new ArgumentException("Invalid Entity type.");
            Id = rescuedAnimal.Id;
            AdopterId = rescuedAnimal.AdopterId;
            RescuedAnimalAdoptionStatus = rescuedAnimal.RescuedAnimalAdoptionStatus;
        }
    }
}
