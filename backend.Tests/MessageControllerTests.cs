using backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace backend.Tests
{
    public class MessageControllerTests
    {
        private readonly Mock<ILogger<MessageController>> _loggerMock;
        private readonly MessageController _controller;

        public MessageControllerTests()
        {
            _loggerMock = new Mock<ILogger<MessageController>>();
            _controller = new MessageController(_loggerMock.Object);
        }

        [Fact]
        public void Get_ReturnsOkWithHelloMessage()
        {
            // Act
            var result = _controller.Get();

            // Assert response type
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            // Assert payload contains expected message
            var payload = okResult.Value as dynamic;
            Assert.Equal("Hello from C# backend!", (string)payload.message);
        }
    }
}
