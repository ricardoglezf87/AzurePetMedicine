namespace AzurePetMedicine.Pets.Domain.ValueObjects
{
    public record PetKind
    {
        public string Value { get; init; }
        public PetKind(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Pet kind cannot be empty.", nameof(value));
            }
            Value = value;
        }

        public static implicit operator string(PetKind petKind) => petKind.Value;

        public static implicit operator PetKind(string value) => new PetKind(value);

        public override string ToString() => Value;

        private static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
