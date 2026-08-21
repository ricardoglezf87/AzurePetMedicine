using AzurePetMedicine.Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.Hospital.Domain.Repositories
{
    public interface IPatientAggregateStore
    {
        Task SaveAsync(Patient patinet);
        Task<Patient> LoadAsync(Guid id);
    }
}
