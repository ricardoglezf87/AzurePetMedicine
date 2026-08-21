using AzurePetMedicine.Common.Events;
using AzurePetMedicine.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.Hospital.Domain.Events;

public record PatientCreatedEvent(Guid id) : IDomainEvent { };
public record PatientAdmittedEvent(Guid id) : IDomainEvent { };
public record PatientDischargedEvent(Guid id) : IDomainEvent { };
public record PatientBloodTypeSetEvent(Guid id, PatientBloodType BloodType) : IDomainEvent { };
public record PatientWeightSetEvent(Guid id, PatientWeight Weight) : IDomainEvent { };
