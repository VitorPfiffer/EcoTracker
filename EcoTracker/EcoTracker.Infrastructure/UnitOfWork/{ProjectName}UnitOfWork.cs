using EcoTracker.Core.Infrastructure.UnitOfWork;
using EcoTracker.Domain.Interfaces.UnitOfWork;
using EcoTracker.Infrastructure.Context;

namespace EcoTracker.Infrastructure.UnitOfWork
{
    public sealed class EcoTrackerUnitOfWork(EcoTrackerContext context) : UnitOfWork<EcoTrackerContext>(context), IEcoTrackerUnitOfWork
    {
    }
}
