using EcoTracker.Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.Core.Controller
{
    [DefaultRoute]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
    }
}
