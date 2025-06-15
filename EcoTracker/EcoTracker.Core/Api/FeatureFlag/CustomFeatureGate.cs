using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;

namespace EcoTracker.Core.Api.FeatureFlag
{
    public class CustomFeatureGate(params string[] features) : FeatureGateAttribute(features)
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
