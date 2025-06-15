using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.API.Controllers
{
    public sealed class UsersController(IUserAppService userAppService) : ApiController
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<UserViewModel?> GetByIdAsync(Guid id) => await userAppService.GetByIdAsync(id);

        [HttpPost]
        public async Task AddAsync([FromBody] AddUserViewModel model) => await userAppService.AddAsync(model);

        [HttpPut("{id}")]
        [Authorize]
        public async Task UpdateAsync(Guid id, [FromBody] UpdateUserViewModel model) => await userAppService.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteAsync(Guid id) => await userAppService.DeleteAsync(id);

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserViewModel>> GetPagedAsync([FromQuery] PagedQuery queryParameters) => await userAppService.GetPagedAsync(queryParameters);
    }
}

