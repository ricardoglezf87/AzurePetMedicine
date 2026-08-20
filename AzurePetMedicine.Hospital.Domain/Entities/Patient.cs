using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Common.Events;
using AzurePetMedicine.Hospital.Domain.Events;
using AzurePetMedicine.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.Hospital.Domain.Entities
{
    public class Patient : AggregateRoot
    {
        public Guid Id { get; set; }
        public PatientBloodType BloodType { get; set; }
        public PatientWeight Weight { get; set; }
        public PatientStatus Status { get; set; }

        public Patient(Guid id)
        {
            ApplyDomainEvent(new PatientCreatedEvent(id));
        }

        public Patient()
        {            
        }

        protected override void ChangeState(IDomainEvent domainEvent)
        {
            switch(domainEvent)
            {
                case PatientCreatedEvent e:
                    Id = e.id;
                    Status = PatientStatus.Pending;
                    break;
                case PatientAdmittedEvent admittedEvent:
                    Status = PatientStatus.Admitted;
                    break;
                case PatientDischargedEvent dischargedEvent:
                    Status = PatientStatus.Discharged;
                    break;
                case PatientBloodTypeSetEvent bloodTypeSetEvent:
                    BloodType = bloodTypeSetEvent.BloodType;
                    break;
                case PatientWeightSetEvent weightSetEvent:
                    Weight = weightSetEvent.Weight;
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled event type: {domainEvent.GetType().Name}");
            }
        }

        protected override void ValidateState()
        {
            var isValid = (Status != PatientStatus.Admitted) || (Id != Guid.Empty && BloodType != null && Weight != null);
            if (!isValid)
            {
                throw new InvalidOperationException("Invalid patient state.");
            }
        }
    }
}
