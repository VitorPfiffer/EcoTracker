using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Attributes;
using EcoTracker.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoTracker.API.Controllers
{
    public class AuthController(IAuthAppService authAppService) : ApiController
    {
        [HttpPost("login")]
        [CustomResponseCode(HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<string> LoginAsync([FromBody] LoginUserViewModel model) => await authAppService.LoginAsync(model.Email, model.Password);
    }
}
