using Moq;
using Domain.Interfaces;
using Domain.Models;
using App.Services;

namespace TestUnit
{
    public class RouteServiceTests
    {
        private readonly Mock<IRouteRepository> _mockRepo;
        private readonly RouteService _service;
        private List<Route> _routes;

        public RouteServiceTests()
        {
            _mockRepo = new Mock<IRouteRepository>();
            _service = new RouteService(_mockRepo.Object);
            _routes = new List<Route>
            {
                new() { Origin = "GRU", Destination = "BRC", Cost = 10 },
                new() { Origin = "BRC", Destination = "SCL", Cost = 5 },
                new() { Origin = "GRU", Destination = "CDG", Cost = 75 },
                new() { Origin = "GRU", Destination = "SCL", Cost = 20 },
                new() { Origin = "GRU", Destination = "ORL", Cost = 56 },
                new() { Origin = "ORL", Destination = "CDG", Cost = 5 },
                new() { Origin = "SCL", Destination = "ORL", Cost = 20 }
            };
        }

        [Fact]
        public async Task GetBestRoute_ShouldReturnCheapestRouteGRUCDG()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllRoutes()).ReturnsAsync(_routes);

            // Act
            BestRouteResponse result = await _service.GetBestRoute("GRU", "CDG");

            // Assert
            Assert.Equal("GRU - BRC - SCL - ORL - CDG", result.Route);
            Assert.Equal(40, result.TotalCost);
        }

        [Fact]
        public async Task GetBestRoute_ShouldReturnCheapestRouteBRCSCL()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllRoutes()).ReturnsAsync(_routes);

            // Act
            BestRouteResponse result = await _service.GetBestRoute("BRC", "SCL");

            // Assert
            Assert.Equal("BRC - SCL", result.Route);
            Assert.Equal(5, result.TotalCost);
        }

        [Fact]
        public async Task GetBestRoute_WhenNoRouteExists_ShouldReturnNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllRoutes()).ReturnsAsync(_routes);

            // Act
            var result = await _service.GetBestRoute("GRU", "CDGX");

            // Assert
            Assert.Null(result);
        }
    }
}