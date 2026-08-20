using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Hospital.Api.Command;
using AzurePetMedicine.Hospital.Domain.Entities;
using AzurePetMedicine.Hospital.Domain.ValueObjects;
using AzurePetMedicine.ServiceBus.Infrastructure;
using System.Transactions;

namespace AzurePetMedicine.Hospital.Api.ApplicationServices
{   
    public class PatientApplicationServices 
    {
        private readonly IGenericRepository<Patient> _repository;

        public PatientApplicationServices(
            IGenericRepository<Domain.Entities.Patient> repository)            
        {     
            _repository = repository;
        }

        public async Task HandleCommandAsync(AdmitPatientCommand command)
        {
            var patient = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Patient with ID {command.id} not found.");
            patient.Status = PatientStatus.Admitted;
            await _repository.UpdateAsync(patient);
        }

        public async Task HandleCommandAsync(DischargePatientCommand command)
        {
            var patient = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Patient with ID {command.id} not found.");
            patient.Status = PatientStatus.Discharged;
            await _repository.UpdateAsync(patient);
        }

        public async Task HandleCommandAsync(SetBloodTypeCommand command)
        {
            var patient = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Patient with ID {command.id} not found.");
            patient.BloodType = command.bloodType;
            await _repository.UpdateAsync(patient);
        }

        public async Task HandleCommandAsync(SetWeightCommand command)
        {
            var patient = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Patient with ID {command.id} not found.");
            patient.Weight = command.weight;
            await _repository.UpdateAsync(patient);
        }

    }
}