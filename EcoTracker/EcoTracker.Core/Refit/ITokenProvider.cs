namespace EcoTracker.Core.Refit
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync(CancellationToken cancellationToken);
    }
}
