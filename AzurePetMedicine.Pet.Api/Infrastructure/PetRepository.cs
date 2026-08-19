using AzurePetMedicine.Common.Domains;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Pet.Api.Infrastructure
{
    public class PetRepository : IGenericRepository<Domain.Entities.Pet>
    {
        private readonly PetDbContext _context;

        public PetRepository(PetDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Pet?> GetByIdAsync(Guid id)
        {
            return await _context.Pets.FindAsync(id);            
        }

        public async Task AddAsync(Domain.Entities.Pet entity)
        {
            await _context.Pets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Entities.Pet>> GetAllAsync()
        {
            return await _context.Pets.ToListAsync<Domain.Entities.Pet>();
        }
       
    }
}
