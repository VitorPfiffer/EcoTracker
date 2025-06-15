using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.Attributes
{
    /// <summary>
    /// Limit the amount of requests that your api support.
    /// <br></br>
    /// <br></br>
    /// <strong>Note:</strong> Only works if your application has just one instance running on server
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LimitRequestAttribute : ActionFilterAttribute
    {
        private readonly int _requestLimit;
        private readonly bool _allowSimultaneRequests;
        private readonly int? _maxSimultaneRequests;
        private readonly string _customSimultaneRequestIdentifier;

        /// <summary>
        /// Limit the amount of requests that your api support
        /// <br></br>
        /// <br></br>
        /// <list type="number">
        ///     <item><strong>Note:</strong> If <paramref name="maxSimultaneRequests"/> not informed, infinity simultane requests are allowed</item>
        ///     <item><strong>Note:</strong> Only works if your application has just one instance running on server</item>
        /// </list>
        /// </summary>
        /// <param name="requestLimit">Limit of requests running</param>
        /// <param name="allowSimultaneRequests">Allow multiple API's call's</param>
        /// <param name="maxSimultaneRequests">The max amount of API's call's simultaneously</param>
        /// <param name="customSimultaneRequestIdentifier">The parameter in the request that be used of identifier for simultane request control</param>
        public LimitRequestAttribute(int requestLimit = 1, bool allowSimultaneRequests = false, int maxSimultaneRequests = 9999, string customSimultaneRequestIdentifier = null)
        {
            _requestLimit = requestLimit;
            _allowSimultaneRequests = allowSimultaneRequests;
            _maxSimultaneRequests = maxSimultaneRequests;
            _customSimultaneRequestIdentifier = customSimultaneRequestIdentifier;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            var apiName = context.HttpContext.Request.Path.Value;

            var identifier = Guid.NewGuid().ToString();

            if (_customSimultaneRequestIdentifier != null)
            {
                context.HttpContext.Request.RouteValues.TryGetValue(_customSimultaneRequestIdentifier, out var routeValue);

                if (routeValue is not null)
                    identifier = routeValue.ToString();
                else
                {
                    context.HttpContext.Request.Query.TryGetValue(_customSimultaneRequestIdentifier, out var queryValue);

                    if (!string.IsNullOrWhiteSpace(queryValue))
                        identifier = queryValue.ToString();
                }
            }
            else identifier = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? Guid.NewGuid().ToString();

            if (cache == null)
            {
                base.OnActionExecuting(context);
                return;
            }

            try
            {
                var requestInfo = cache.Get<RequestLimitInfo>(apiName);

                if (requestInfo is null)
                {
                    requestInfo = new RequestLimitInfo(_requestLimit, _allowSimultaneRequests, _maxSimultaneRequests);
                    requestInfo.ApiPath = apiName;

                    if (requestInfo.AllowSimultaneRequests)
                    {
                        requestInfo.RequestsCounter.Add(new RequestLimitInfo.RequestCounter
                        {
                            Count = 1,
                            Key = identifier
                        });
                    }
                    else
                    {
                        requestInfo.RequestsCounter.Add(new RequestLimitInfo.RequestCounter
                        {
                            Count = 1,
                            Key = apiName
                        });
                    }

                    cache.Set(apiName, requestInfo);

                    base.OnActionExecuting(context);
                    return;
                }

                if (requestInfo.AllowSimultaneRequests)
                {
                    if (requestInfo.RequestsCounter.Any(x => x.Key == identifier))
                    {
                        var userRequestCounter = requestInfo.RequestsCounter.FirstOrDefault(x => x.Key == identifier);

                        userRequestCounter.Count += 1;

                        if (userRequestCounter.Count > requestInfo.RequestLimit) return;

                        cache.Set(apiName, requestInfo);

                        base.OnActionExecuting(context);
                        return;
                    }
                    else
                    {
                        requestInfo.RequestsCounter.Add(new RequestLimitInfo.RequestCounter
                        {
                            Count = 1,
                            Key = identifier
                        });

                        if (requestInfo.MaxSimultaneRequests != 9999 &&
                            requestInfo.RequestsCounter.Count > requestInfo.MaxSimultaneRequests)
                            return;

                        cache.Set(apiName, requestInfo);

                        base.OnActionExecuting(context);
                        return;
                    }
                }

                var requestCounter = requestInfo.RequestsCounter.FirstOrDefault(x => x.Key == apiName);

                requestCounter.Count += 1;

                if (requestCounter.Count > requestInfo.RequestLimit) return;

                cache.Set(apiName, requestInfo);

                base.OnActionExecuting(context);
            }
            catch
            {
                cache.Remove(apiName);

                throw;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            var apiName = context.HttpContext.Request.Path;

            if (cache == null)
            {
                base.OnActionExecuted(context);
                return;
            }

            cache.Remove(apiName);

            base.OnActionExecuted(context);
        }
    }

    public class RequestLimitInfo
    {
        public RequestLimitInfo(int requestLimit, bool allowSimultaneRequests, int? maxSimultaneRequests)
        {
            RequestLimit = requestLimit;
            AllowSimultaneRequests = allowSimultaneRequests;
            MaxSimultaneRequests = maxSimultaneRequests;
        }

        public string ApiPath { get; set; }
        public int RequestLimit { get; set; } = 1;
        public bool AllowSimultaneRequests { get; set; } = false;
        public int? MaxSimultaneRequests { get; set; } = null;
        public List<RequestCounter> RequestsCounter { get; set; } = new List<RequestCounter>();

        public class RequestCounter
        {
            public string Key { get; set; }
            public int Count { get; set; }
        }
    }

    public static class DependencyInjection
    {
        public static IServiceCollection AddRequestLimiter(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }
    }
}
