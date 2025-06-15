using EcoTracker.Core.Api.Pagination;
using EcoTracker.Domain.Entities;

namespace EcoTracker.Domain.Interfaces.Services
{
    public interface IUserDomainService
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User entity);
        Task<User?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<IEnumerable<User>> GetPagedAsync(PagedQuery queryParameters);
    }
}
