using EcoTracker.Domain.Enums;

namespace EcoTracker.Application.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }

        public required string Username { get; set; }

        public required Roles Role { get; set; }

    }
}
