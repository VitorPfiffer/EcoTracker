using EcoTracker.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.Core.Attributes
{
    public class CustomRouteAttribute : RouteAttribute
    {
        public CustomRouteAttribute(string route) : base(ControllersInfo.GetFinalUrl() + route) { }
    }
}
