using EcoTracker.Core.Data;
using EcoTracker.Core.Enums;
using EcoTracker.Core.NotificationManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EcoTracker.Core.Api
{
    public sealed class ApiResponseNormalizerActionFilter(ApiResponse apiResponse, INotificationManager notificationManager) : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized) return;

            if (context.Canceled) return;

            var httpContext = context.HttpContext;

            if (httpContext.Request.Path.Value.Contains("swagger"))
                return;

            var translatableNotificationManager = notificationManager as ITranslatableNotificationManager;

            translatableNotificationManager.TranslateMessages();

            var errors = translatableNotificationManager.Get(NotificationType.Error).Select(x => x.Message);

            if (errors.Any())
                apiResponse.Errors = [.. errors];
            else
            {
                try
                {
                    apiResponse.Data = (context.Result as ObjectResult)?.Value;
                }
                catch
                {
                }
            }

            if (apiResponse.Success)
            {
                switch (httpContext.Request.Method.ToUpper())
                {
                    case "GET":
                        if (apiResponse.Data != null)
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        }
                        else
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                        }

                        break;
                    case "POST":
                    case "PUT":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                        break;

                    case "PATCH":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        break;

                    case "DELETE":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;

                    default:
                        if (apiResponse.Data != null)
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        }
                        else
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                        }
                        break;
                }
            }
            else
            {
                if (apiResponse.HasUnhandledException)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                else if (apiResponse?.Errors?.Any() ?? false)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }

            context.Result = new ObjectResult(apiResponse);
        }

        public void OnActionExecuting(ActionExecutingContext httpContext)
        {
        }
    }
}
