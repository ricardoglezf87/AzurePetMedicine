using System;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.Hospital.Domain.ValueObjects
{
    public record PatientWeight
    {
        public Decimal? Value { get; init; }
        public PatientWeight(Decimal? value)
        {
            Value = value;
        }

        public static implicit operator Decimal?(PatientWeight patientWeight) => patientWeight.Value;

        public static implicit operator PatientWeight(Decimal? value) => new PatientWeight(value);

        private static bool IsValid(Decimal? value)
        {
            return (value >= 0);
        }
    }
}
