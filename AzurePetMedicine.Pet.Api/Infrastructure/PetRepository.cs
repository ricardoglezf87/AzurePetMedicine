using System;
using System.Threading.Tasks;
using AzurePetMedicine.Pet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Pet.Api.Infrastructure
{
    public class PetRepository : IPetRepository
    {
        private readonly PetDbContext _context;

        public PetRepository(PetDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Pet?> GetPetAsync(Guid id)
        {
            return await _context.Pets.FindAsync(id);            
        }

        public async Task AddPetAsync(Domain.Entities.Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePetAsync(Guid id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Entities.Pet>> GetAllPetsAsync()
        {
            return await _context.Pets.ToListAsync<Domain.Entities.Pet>();
        }
    }
}
