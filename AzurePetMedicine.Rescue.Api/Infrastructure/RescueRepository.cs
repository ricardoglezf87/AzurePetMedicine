using AzurePetMedicine.Common.Domains;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Rescue.Api.Infrastructure
{
    public class RescueRepository : IGenericRepository<Domain.Entities.Adopter>
    {
        private readonly RescueDbContext _context;

        public RescueRepository(RescueDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Adopter?> GetByIdAsync(Guid id)
        {
            return await _context.Adopters.FindAsync(id);            
        }

        public async Task AddAsync(Domain.Entities.Adopter entity)
        {
            await _context.Adopters.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Adopter rescue)
        {
            _context.Adopters.Update(rescue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var rescue = await _context.Adopters.FindAsync(id);
            if (rescue != null)
            {
                _context.Adopters.Remove(rescue);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Entities.Adopter>> GetAllAsync()
        {
            return await _context.Adopters.ToListAsync<Domain.Entities.Adopter>();
        }
       
    }
}
