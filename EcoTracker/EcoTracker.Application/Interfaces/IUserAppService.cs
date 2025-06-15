using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;

namespace EcoTracker.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserViewModel?> GetByUsernameAsync(string username);
        Task AddAsync(AddUserViewModel model);
        Task<UserViewModel?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid Id);
        Task UpdateAsync(Guid id, UpdateUserViewModel model);
        Task<IEnumerable<UserViewModel>> GetPagedAsync(PagedQuery queryParameters);

    }
}
