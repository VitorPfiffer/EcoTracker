using AutoMapper;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Application;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Services;
using EcoTracker.Domain.Interfaces.UnitOfWork;

namespace EcoTracker.Application.Services
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserDomainService _userDomainService;
        private readonly IEcoTrackerUnitOfWork _unitOfWork;

        public UserAppService(INotificationManager notificationManager, IMapper mapper, IUserDomainService userDomainService, IEcoTrackerUnitOfWork unitOfWork) : base(notificationManager, mapper)
        {
            _userDomainService = userDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserViewModel?> GetByUsernameAsync(string username)
        {
            var user = await _userDomainService.GetByUsernameAsync(username);

            if (user == null)
                return null;

            var viewModel = _mapper.Map<UserViewModel>(user);
            return viewModel;
        }

        public async Task<UserViewModel?> GetByIdAsync(Guid id)
        {
            var user = await _userDomainService.GetByIdAsync(id);

            var viewModel = _mapper.Map<UserViewModel>(user);

            return viewModel;
        }


        public async Task AddAsync(AddUserViewModel model)
        {
            var user = _mapper.Map<User>(model);

            await _userDomainService.AddAsync(user);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateUserViewModel model)
        {

            var userDb = await _userDomainService.GetByIdAsync(id);

            if (userDb == null) return;

            var user = _mapper.Map<User>(model);
            user.SetId(id);

            user = _mapper.Map(user, userDb);

            await _userDomainService.UpdateAsync(user);

            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(Guid Id)
        {
            var user = await _userDomainService.GetByIdAsync(Id);

            if (user == null) return;
            await _userDomainService.DeleteAsync(user);
            await _unitOfWork.CommitAsync();

        }
        public async Task<IEnumerable<UserViewModel>> GetPagedAsync(PagedQuery queryParameters)
        {
            var userList = await _userDomainService.GetPagedAsync(queryParameters);

            return _mapper.Map<IEnumerable<UserViewModel>>(userList);
        }
    }
}
