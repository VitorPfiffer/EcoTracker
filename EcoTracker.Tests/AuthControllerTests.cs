using EcoTracker.API.Controllers;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using Moq;

namespace EcoTracker.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task LoginAsync_ReturnsToken_And_Status200()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthAppService>();
            var expectedToken = "mocked-jwt-token";
            var loginViewModel = new LoginUserViewModel
            {
                Email = "usuario@teste.com",
                Password = "senhaSegura123"
            };

            mockAuthService
                .Setup(service => service.LoginAsync(loginViewModel.Email, loginViewModel.Password))
                .ReturnsAsync(expectedToken);

            var controller = new AuthController(mockAuthService.Object);

            // Act
            var result = await controller.LoginAsync(loginViewModel);

            // Assert
            Assert.Equal(expectedToken, result);
        }
    }
}