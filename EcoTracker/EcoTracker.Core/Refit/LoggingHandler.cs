using EcoTracker.Core.NotificationManager;
using Microsoft.Extensions.Logging;

namespace EcoTracker.Core.Refit
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly INotificationManager _notificationManager;
        private readonly ILogger<LoggingHandler> _logger;

        public LoggingHandler(INotificationManager notificationManager, ILogger<LoggingHandler> logger)
        {
            _notificationManager = notificationManager;
            _logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await base.SendAsync(request, cancellationToken);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

                string requestBody = "";
                if (request.Content != null)
                    requestBody = $"RequestBody: {await request.Content.ReadAsStringAsync(cancellationToken)}";

                _logger.LogError($"Request failed with status code {(int)httpResponseMessage.StatusCode} {httpResponseMessage.StatusCode}.", requestBody, request.ToString());
            }


            return httpResponseMessage;
        }



    }
}
