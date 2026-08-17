using AzurePetMedicine.Pet.Api.Command;
using AzurePetMedicine.Pet.Domain.Events;
using AzurePetMedicine.Pet.Domain.IntegrationEvents;
using AzurePetMedicine.Pet.Domain.Repositories;
using AzurePetMedicine.Pet.Domain.ValueObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AzurePetMedicine.Pet.Api.ApplicationServices
{   
    public class PetApplicationServices
    {
        private readonly IPetRepository PetRepository;

        public PetApplicationServices(IPetRepository petRepository)
        {
            PetRepository = petRepository;
            DomainEvents.PetFlaggedForAdoption.Register( c=>
            {
                var integrationEventHandler = new PetFlaggedForAdoptionIntegrationEvent(c.Id, c.Name, c.Kind, c.Age);
            });
        }

        public async Task HandleCommandAsync(CreatePetCommand command)
        {
            var pet = new Domain.Entities.Pet(PetId.Create());
            pet.Name = command.Name;
            pet.Kind = command.Kind;
            pet.Age = command.Age;
            await PetRepository.AddPetAsync(pet);
        }

        public async Task HandleCommandAsync(UpdatePetCommand command)
        {
            var petId = new PetId(command.Id);
            var pet = await PetRepository.GetPetAsync(petId);
            if (pet == null)
            {
                throw new Exception($"Pet with Id {petId} not found.");
            }
            pet.Name = command.Name;
            pet.Kind = command.Kind;
            pet.Age = command.Age;
            await PetRepository.UpdateAsync(pet);
        }

        public async Task HandleCommandAsync(DeletePetCommand command)
        {
            var petId = new PetId(command.Id);
            var pet = await PetRepository.GetPetAsync(petId);
            if (pet == null)
            {
                throw new Exception($"Pet with Id {petId} not found.");
            }
            await PetRepository.DeletePetAsync(command.Id);
        }

        public async Task HandleCommandAsync(FlagPetForAdoptionCommand command)
        {
            var petId = new PetId(command.Id);
            var pet = await PetRepository.GetPetAsync(petId);
            if (pet == null)
            {
                throw new Exception($"Pet with Id {petId} not found.");
            }
            pet.FlagForAdoption();
            await PetRepository.UpdateAsync(pet);
        }

        public async Task<Domain.Entities.Pet> HandleQueryAsync(GetPetByIdQuery query)
        {
            var petId = new PetId(query.Id); 

            var pet = await PetRepository.GetPetAsync(petId);
            if (pet == null)
            {
                throw new KeyNotFoundException($"Pet with Id {query.Id} not found.");
            }
            return pet;
        }

        public async Task<List<Domain.Entities.Pet>> HandleQueryAsync(GetAllPetsQuery query)
        {
            return await PetRepository.GetAllPetsAsync();
        }
    }
}