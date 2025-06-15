namespace EcoTracker.Core.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
