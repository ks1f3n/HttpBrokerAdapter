using System.Net.Http;
using System.Threading.Tasks;
using HttpToBrokerAdapter.Controllers;
using HttpToBrokerAdapter.Dtos;
using HttpToBrokerAdapter.Interfaces;
using HttpToBrokerAdapter.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HttpToBrokerAdapterUnitTest
{
    public class ValuesControllerTest
    {
        [Fact]
        public async Task CreateReturnIdAsync()
        {
            // Arrange
            var stubLogger = new Mock<ILogger<ValuesController>>();
            var controller = new ValuesController(stubLogger.Object);

            // Act
            var result = await controller.CreateGapAsync(new CreateGapSensorDto() {Id = 10});

            // Assert
            Assert.NotNull(result);
        }
    }
}
