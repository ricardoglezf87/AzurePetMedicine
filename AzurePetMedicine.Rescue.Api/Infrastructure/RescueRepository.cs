using AzurePetMedicine.Common.Domains;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Rescue.Api.Infrastructure
{
    public class RescueRepository : IGenericRepository<Domain.Entities.Rescue>
    {
        private readonly RescueDbContext _context;

        public RescueRepository(RescueDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Rescue?> GetByIdAsync(Guid id)
        {
            return await _context.Rescues.FindAsync(id);            
        }

        public async Task AddAsync(Domain.Entities.Rescue entity)
        {
            await _context.Rescues.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Rescue rescye)
        {
            _context.Rescues.Update(rescye);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var rescye = await _context.Rescues.FindAsync(id);
            if (rescye != null)
            {
                _context.Rescues.Remove(rescye);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Entities.Rescue>> GetAllAsync()
        {
            return await _context.Rescues.ToListAsync<Domain.Entities.Rescue>();
        }
       
    }
}
