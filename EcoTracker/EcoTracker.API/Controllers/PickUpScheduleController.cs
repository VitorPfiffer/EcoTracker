using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Attributes;
using EcoTracker.Core.Controller;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.API.Controllers
{
    [CustomRoute("pickup-schedule")]
    public sealed class PickUpScheduleController(IPickUpScheduleServiceApp PickUpScheduleServiceApp) : ApiController
    {
        [HttpGet("{id}")]
        public async Task<PickUpScheduleViewModel?> GetByIdAsync(Guid id) => await PickUpScheduleServiceApp.GetByIdAsync(id);

        [HttpPost]
        public async Task AddAsync([FromBody] AddPickUpScheduleViewModel model) => await PickUpScheduleServiceApp.AddAsync(model);

        [HttpPut("{id}")]
        public async Task UpdateAsync(Guid id, [FromBody] UpdatePickUpScheduleViewModel model) => await PickUpScheduleServiceApp.UpdateAsync(id, model);

        [HttpDelete("delete/{id}")]
        public async Task DeleteAsync(Guid id) => await PickUpScheduleServiceApp.DeleteAsync(id);

        [HttpGet]
        public async Task<IEnumerable<PickUpScheduleViewModel>> GetPagedAsync([FromQuery] PagedQuery queryParameters) => await PickUpScheduleServiceApp.GetPagedAsync(queryParameters);
    }
}