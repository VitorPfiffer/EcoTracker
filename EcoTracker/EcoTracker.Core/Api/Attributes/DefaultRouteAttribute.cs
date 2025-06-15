using EcoTracker.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.Core.Attributes
{
    public class DefaultRouteAttribute : RouteAttribute
    {
        public DefaultRouteAttribute() : base(ControllersInfo.GetFinalUrl() + "[controller]") { }
    }
}
