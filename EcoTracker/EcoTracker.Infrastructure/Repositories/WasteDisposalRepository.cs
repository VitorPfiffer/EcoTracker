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
    }
}