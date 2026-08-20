using AzurePetMedicine.Common.Domains;

namespace AzurePetMedicine.Rescue.Domain.Entities
{
    public class Rescue : IEntity, IMappableEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.MinValue;
        public string Description { get; set; } = string.Empty;


        public Rescue()
        {

        }

        public Rescue(Guid id)
        {
            Id = id;
        }

        public void MapFromEntity(object entity)
        {
            if (entity is not Rescue rescue)
                throw new ArgumentException("Invalid Entity type.");
            Id = rescue.Id;
            Date = rescue.Date;
            Description = rescue.Description;
        }
    }
}
