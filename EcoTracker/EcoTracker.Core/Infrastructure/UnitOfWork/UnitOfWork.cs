using EcoTracker.Core.Infrastructure.Context;

namespace EcoTracker.Core.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DatabaseContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context) => _context = context;

        public async Task<bool> CommitAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                bool success = (await _context.SaveChangesAsync()) > 0;

                if (!success)
                {
                    await transaction.RollbackAsync();
                    return success;
                }

                await transaction.CommitAsync();
                return success;
            }
        }

        public void Dispose() => _context.Dispose();
    }
}
