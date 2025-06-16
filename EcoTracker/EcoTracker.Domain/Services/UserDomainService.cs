using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Domain.Services;
using EcoTracker.Core.FluentValidator.ValidatorManager;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Domain.Interfaces.Services;
using EcoTracker.Domain.Validators;

namespace EcoTracker.Domain.Services
{
    public sealed class UserDomainService : DomainService, IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(INotificationManager notificationManager, IValidatorManager validationManager, IUserRepository userRepository) : base(notificationManager, validationManager)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsync(User entity)
        {
            var isValid = await _validatorManager.ValidateAsync<AddUserValidator>(entity);

            if (!isValid)
                return;

            await _userRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await _userRepository.DeleteAsync(entity);
        }

        public async Task<User?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var user = await _userRepository.GetByIdAsync(id, includeDeleted);
            if (user == null)
                NotifyError("user_not_found");

            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }


        public async Task UpdateAsync(User entity)
        {
            var isValid = await _validatorManager.ValidateAsync<UpdateUserValidator>(entity);

            if (!isValid)
                return;

            await _userRepository.UpdateAsync(entity);
        }
        public async Task<IEnumerable<User>> GetPagedAsync(PagedQuery queryParameters)
        {
            return await _userRepository.GetPagedAsync(queryParameters);
        }
    }
}
