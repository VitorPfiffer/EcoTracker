using EcoTracker.Core.Infrastructure.Repository;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Infrastructure.Repositories
{
    public sealed class WasteDisposalRepository(EcoTrackerContext context) : Repository<WasteDisposal>(context), IWasteDisposalRepository
    {

        public override async Task<WasteDisposal?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WasteDisposal?> GetByUserIdAsync(Guid userId, bool includeDeleted = false)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<WasteDisposal>> GetAllAsync()
        {
            return await DbSet.Include(x => x.User).ToListAsync();
        }

        public async Task<IEnumerable<WasteDisposal>> GetByMonthAsync(int year, int month)
        {
            return await DbSet.Where(w => w.Date.Year == year &&
                           w.Date.Month == month)
                .ToListAsync();

        }
    }
}