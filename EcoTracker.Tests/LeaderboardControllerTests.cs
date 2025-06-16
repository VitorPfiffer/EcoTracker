using EcoTracker.API.Controllers;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels.Leaderboard;
using Moq;

namespace EcoTracker.Tests
{
    public class LeaderboardControllerTests
    {
        [Fact]
        public async Task GetLeaderboardAsync_ReturnsLeaderboardList()
        {
            // Arrange
            var mockService = new Mock<ILeaderboardAppService>();
            var expectedList = new List<LeaderboardViewModel>
        {
            new() { Username = "User1", Unit = "kg", WasteTypes = ["lixo"] },
            new() { Username = "User2", Unit = "kg", WasteTypes = ["lixo"] }
        };

            mockService
                .Setup(s => s.GetLeaderboardAsync())
                .ReturnsAsync(expectedList);

            var controller = new LeaderboardController(mockService.Object);

            // Act
            var result = await controller.GetLeaderboardAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedList.Count, ((List<LeaderboardViewModel>)result).Count);
            Assert.Equal("User1", ((List<LeaderboardViewModel>)result)[0].Username);
            Assert.Equal("kg", ((List<LeaderboardViewModel>)result)[0].Unit);
        }
    }
}