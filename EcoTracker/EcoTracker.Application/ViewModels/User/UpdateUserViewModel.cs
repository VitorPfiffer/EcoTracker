using EcoTracker.Domain.Enum;

namespace EcoTracker.Application.ViewModels
{
    public class UpdateUserViewModel
    {
        public required string Email { get; set; }

        public required string Username { get; set; }

        public required Roles Role { get; set; }
    }

}
