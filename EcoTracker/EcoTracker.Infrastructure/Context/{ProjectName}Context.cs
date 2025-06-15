using EcoTracker.Core.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Infrastructure.Context
{
    public class EcoTrackerContext(DbContextOptions options) : DatabaseContext(options)
    {
    }
}
