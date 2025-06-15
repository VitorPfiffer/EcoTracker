
using EcoTracker.Core.Infrastructure;
using EcoTracker.Domain.Enum;

namespace EcoTracker.Domain.Entities
{
    public sealed class User : Entity
    {
        public required string Email { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required Roles Role { get; set; }
    }
}
