using EcoTracker.API.Controllers;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Domain.Enum;
using Moq;

namespace EcoTracker.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsExpectedUser()
        {
            // Arrange
            var mockService = new Mock<IUserAppService>();
            var id = Guid.NewGuid();

            var expectedUser = new UserViewModel
            {
                Id = id,
                Username = "usuarioTeste",
                Email = "usuario@teste.com",
                Role = Roles.Admin
            };

            mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(expectedUser);

            var controller = new UsersController(mockService.Object);

            // Act
            var result = await controller.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result!.Id);
            Assert.Equal(expectedUser.Username, result.Username);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.Role, result.Role);
        }
    }
}