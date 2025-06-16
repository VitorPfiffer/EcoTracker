using EcoTracker.Core.Infrastructure.Repository;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Infrastructure.Repositories
{
    public sealed class PickUpScheduleRepository(EcoTrackerContext context) : Repository<PickUpSchedule>(context), IPickUpScheduleRepository
    {

        public override async Task<PickUpSchedule?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}