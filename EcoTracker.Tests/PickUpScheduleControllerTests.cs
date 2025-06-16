using EcoTracker.API.Controllers;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using Moq;

namespace EcoTracker.Tests
{
    public class PickUpScheduleControllerTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsExpectedSchedule()
        {
            // Arrange
            var mockService = new Mock<IPickUpScheduleServiceApp>();
            var id = Guid.NewGuid();

            var expected = new PickUpScheduleViewModel
            {
                Id = id,
                City = "São Paulo",
                Neighborhood = "Morumbi",
                Number = "123",
                PostalCode = "1234-1234",
                State = "SP",
                Street = "Rua das Árvores",
                WasteType = "Lixo",
                ScheduledDate = DateTime.Now
            };

            mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(expected);

            var controller = new PickUpScheduleController(mockService.Object);

            // Act
            var result = await controller.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Id, result!.Id);
            Assert.Equal(expected.City, result.City);
            Assert.Equal(expected.Neighborhood, result.Neighborhood);
            Assert.Equal(expected.Number, result.Number);
            Assert.Equal(expected.PostalCode, result.PostalCode);
            Assert.Equal(expected.State, result.State);
            Assert.Equal(expected.Street, result.Street);
            Assert.Equal(expected.WasteType, result.WasteType);
            Assert.Equal(expected.ScheduledDate, result.ScheduledDate);
        }
    }
}