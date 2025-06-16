using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Attributes;
using EcoTracker.Core.Controller;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.API.Controllers
{
    [CustomRoute("waste-disposals")]
    public sealed class WasteDisposalsController(IWasteDisposalServiceApp WasteDisposalServiceApp) : ApiController
    {
        [HttpGet("{id}")]
        public async Task<WasteDisposalViewModel?> GetByIdAsync(Guid id) => await WasteDisposalServiceApp.GetByIdAsync(id);

        [HttpPost]
        public async Task AddAsync([FromBody] AddWasteDisposalViewModel model) => await WasteDisposalServiceApp.AddAsync(model);

        [HttpPut("{id}")]
        public async Task UpdateAsync(Guid id, [FromBody] UpdateWasteDisposalViewModel model) => await WasteDisposalServiceApp.UpdateAsync(id, model);

        [HttpDelete("delete/{id}")]
        public async Task DeleteAsync(Guid id) => await WasteDisposalServiceApp.DeleteAsync(id);

        [HttpGet]
        public async Task<IEnumerable<WasteDisposalViewModel>> GetPagedAsync([FromQuery] PagedQuery queryParameters) => await WasteDisposalServiceApp.GetPagedAsync(queryParameters);

        [HttpGet("user/{userId}")]
        public async Task<WasteDisposalViewModel?> GetByUserIdAsync(Guid userId) => await WasteDisposalServiceApp.GetByUserIdAsync(userId);
    }
}