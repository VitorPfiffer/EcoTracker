using EcoTracker.Core.Infrastructure.Repository;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Infrastructure.Repositories
{
    public sealed class UserRepository(EcoTrackerContext context) : Repository<User>(context), IUserRepository
    {
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Username == username);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
        public override async Task<User?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {

            var teste = await DbSet.ToListAsync();
            return await DbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
