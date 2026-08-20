using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Rescue.Api.Command;
using AzurePetMedicine.Rescue.Domain.Entities;
using AzurePetMedicine.Rescue.Domain.ValueObjects;
using AzurePetMedicine.ServiceBus.Infrastructure;
using System.Transactions;

namespace AzurePetMedicine.Rescue.Api.ApplicationServices
{   
    public class AdopterApplicationServices 
    {
        private readonly IGenericRepository<Adopter> _repository;

        public AdopterApplicationServices(
            IGenericRepository<Domain.Entities.Adopter> repository)            
        {     
            _repository = repository;
        }

        public async Task HandleCommandAsync(CreateAdopterCommand command)
        {
            var adopter = new Adopter()
            {
                Id = Guid.NewGuid(),
                Name = command.name,
                PhoneNumber = command.phoneNumber
            };

            await _repository.AddAsync(adopter);
        }

        public async Task HandleCommandAsync(SetAdopterPhoneNumberCommand command)
        {
            var adopter = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Adopter with ID {command.id} not found.");            
            adopter.PhoneNumber = command.phoneNumber;
            await _repository.UpdateAsync(adopter);
        }


    }
}