namespace AzurePetMedicine.Pet.Domain.ValueObjects
{
    public class PetId
    {
        public Guid Value { get; init; } 
        
        public PetId(Guid value)
        {
            Validate(value);
            Value = value;
        }

        public static PetId Create() => new PetId(Guid.NewGuid());
        

        public static implicit operator Guid(PetId petId) => petId.Value;


        private static void Validate(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("PetId cannot be empty.");
            }
        }
    }
}
