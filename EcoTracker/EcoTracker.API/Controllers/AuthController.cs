using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.API.Controllers
{
    public class AuthController(IAuthAppService authAppService) : ApiController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<string> LoginAsync([FromBody] LoginUserViewModel model) => await authAppService.LoginAsync(model.Email, model.Password);
    }
}
