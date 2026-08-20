using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Rescue.Domain.ValueObjects;

namespace AzurePetMedicine.Rescue.Domain.Entities
{
    public class Adopter : IEntity, IMappableEntity
    {
        public Guid Id { get; set; }
        public AdopterName Name { get; set; } 
        public AdopterPhoneNumber PhoneNumber { get; set; }


        public Adopter()
        {

        }

        public Adopter(Guid id)
        {
            Id = id;
        }

        public void MapFromEntity(object entity)
        {
            if (entity is not Adopter rescue)
                throw new ArgumentException("Invalid Entity type.");
            Id = rescue.Id;
            Name = rescue.Name;
            PhoneNumber = rescue.PhoneNumber;
        }
    }
}
