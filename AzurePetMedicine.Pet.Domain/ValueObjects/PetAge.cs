namespace AzurePetMedicine.Pets.Domain.ValueObjects
{
    public record PetAge
    {
        public int Value { get; init; }
        public PetAge(int value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Pet age cannot be negative.", nameof(value));
            }
            Value = value;
        }

        public static implicit operator int(PetAge petAge) => petAge.Value;

        public static implicit operator PetAge(int value) => new PetAge(value);

        private static bool IsValid(int value)
        {
            return value >= 0;
        }   
    }
}
