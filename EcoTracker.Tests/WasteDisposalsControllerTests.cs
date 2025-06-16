using EcoTracker.API.Controllers;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using Moq;

namespace EcoTracker.Tests
{
    public class WasteDisposalsControllerTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsExpectedWasteDisposal()
        {
            // Arrange
            var mockService = new Mock<IWasteDisposalServiceApp>();
            var id = Guid.NewGuid();

            var expected = new WasteDisposalViewModel
            {
                Id = id,
                WasteType = "Plástico",
                Quantity = 2,
                Unit = "kg",
                Date = DateTime.Today,
            };
            mockService
                .Setup(s => s.GetByIdAsync(id))
                .ReturnsAsync(expected);

            var controller = new WasteDisposalsController(mockService.Object);

            var result = await controller.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(expected.Id, result!.Id);
            Assert.Equal(expected.WasteType, result.WasteType);
            Assert.Equal(expected.Quantity, result.Quantity);
            Assert.Equal(expected.Unit, result.Unit);
            Assert.Equal(expected.Date, result.Date);
        }
    }
}