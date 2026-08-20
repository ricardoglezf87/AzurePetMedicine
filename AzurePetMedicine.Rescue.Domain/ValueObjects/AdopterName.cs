namespace AzurePetMedicine.Rescue.Domain.ValueObjects
{
    public record AdopterName
    {
        public string Value { get; init; }
        public AdopterName(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Adopter name cannot be empty.", nameof(value));
            }
            Value = value;
        }

        public static implicit operator string(AdopterName adopterName) => adopterName.Value;

        public static implicit operator AdopterName(string value) => new AdopterName(value);

        public override string ToString() => Value;

        private static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
