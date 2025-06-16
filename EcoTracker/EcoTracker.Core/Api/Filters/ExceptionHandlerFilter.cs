using EcoTracker.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EcoTracker.Core.Api.ActionFilters
{
    public sealed class ExceptionHandlerFilter(ApiResponse apiResponse, ILogger<ExceptionHandlerFilter> logger) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "Development" || environment == "Local")
                throw context.Exception;

            logger.LogError(context.Exception, context.Exception?.InnerException?.Message);

            apiResponse.HasUnhandledException = true;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Result = new ObjectResult(apiResponse);
        }
    }
}
