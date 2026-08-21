using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Hospital.Api.Command;
using AzurePetMedicine.Hospital.Domain.Entities;
using AzurePetMedicine.Hospital.Domain.Repositories;
using AzurePetMedicine.Hospital.Domain.ValueObjects;
using AzurePetMedicine.ServiceBus.Infrastructure;
using System.Transactions;

namespace AzurePetMedicine.Hospital.Api.ApplicationServices
{   
    public class PatientApplicationServices 
    {
        private readonly IPatientAggregateStore _patientAggregateStore;

        public PatientApplicationServices(
            IPatientAggregateStore patientAggregateStore)            
        {
            _patientAggregateStore = patientAggregateStore;
        }

        public async Task HandleCommandAsync(AdmitPatientCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(command.id);
            patient.AdmitPatient();
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleCommandAsync(DischargePatientCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(command.id);            
            patient.DischargedPatient();
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleCommandAsync(SetBloodTypeCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(command.id);
            patient.SetBloodType(new PatientBloodType(command.bloodType));
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleCommandAsync(SetWeightCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(command.id);
            patient.SetWeight(new PatientWeight(command.weight));
            await _patientAggregateStore.SaveAsync(patient);
        }

    }
}