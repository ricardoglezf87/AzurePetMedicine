namespace AzurePetMedicine.Rescue.Domain.ValueObjects
{
    public record AdopterPhoneNumber
    {
        public string Value { get; init; }
        public AdopterPhoneNumber(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Adopter phone number cannot be empty.", nameof(value));
            }
            Value = value;
        }

        public static implicit operator string(AdopterPhoneNumber adopterPhoneNumber) => adopterPhoneNumber.Value;

        public static implicit operator AdopterPhoneNumber(string value) => new AdopterPhoneNumber(value);

        public override string ToString() => Value;

        private static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
