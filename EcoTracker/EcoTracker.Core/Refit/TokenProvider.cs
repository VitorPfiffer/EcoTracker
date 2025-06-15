

using System.Net.Http.Headers;

namespace EcoTracker.Core.Refit
{
    public abstract class TokenProvider : DelegatingHandler, ITokenProvider
    {
        public abstract Task<string> GetTokenAsync(CancellationToken cancellationToken);

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetTokenAsync(cancellationToken);

            request.Headers.Authorization = request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
