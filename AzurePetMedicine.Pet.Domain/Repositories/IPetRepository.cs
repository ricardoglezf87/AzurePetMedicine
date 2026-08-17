using AzurePetMedicine.Pet.Domain.Entities;

namespace AzurePetMedicine.Pet.Domain.Repositories
{
    public interface IPetRepository
    {
        public Task<Entities.Pet> GetPetAsync(Guid id);
        public Task AddPetAsync(Entities.Pet pet);
        public Task UpdateAsync(Entities.Pet pet);
        public Task DeletePetAsync(Guid id);
        public Task<List<Entities.Pet>> GetAllPetsAsync();
    }
}
