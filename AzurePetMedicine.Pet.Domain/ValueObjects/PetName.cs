namespace AzurePetMedicine.Pets.Domain.ValueObjects
{
    public record PetName
    {
        public string Value { get; init; }
        public PetName(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Pet name cannot be empty.", nameof(value));
            }
            Value = value;
        }

        public static implicit operator string(PetName petName) => petName.Value;

        public static implicit operator PetName(string value) => new PetName(value);

        public override string ToString() => Value;

        private static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
