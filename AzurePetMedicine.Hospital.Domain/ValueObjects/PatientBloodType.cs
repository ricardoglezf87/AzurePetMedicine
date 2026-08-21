using System;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.Hospital.Domain.ValueObjects
{
    public record PatientBloodType
    {
        public string? Value { get; init; }
        public PatientBloodType(string? value)
        {
            Value = value;
        }

        public static implicit operator string?(PatientBloodType patientBloodType) => patientBloodType.Value;

        public static implicit operator PatientBloodType(string? value) => new PatientBloodType(value);

        public override string ToString() => Value;

    }
}
