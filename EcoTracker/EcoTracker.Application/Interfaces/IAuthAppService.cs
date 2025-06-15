using EcoTracker.Domain.Entities;

namespace EcoTracker.Application.Interfaces
{
    public interface IAuthAppService
    {
        string GenerateToken(User user);
        Task<string> LoginAsync(string username, string password);
    }
}
