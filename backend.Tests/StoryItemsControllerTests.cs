using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Controllers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace backend.Tests
{
    public class StoryItemsControllerTests
    {
        private readonly Mock<IStoryItemService> _serviceMock;
        private readonly Mock<ILogger<StoryItemsController>> _loggerMock;
        private readonly StoryItemsController _controller;

        public StoryItemsControllerTests()
        {
            _serviceMock = new Mock<IStoryItemService>();
            _loggerMock = new Mock<ILogger<StoryItemsController>>();
            _controller = new StoryItemsController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetStoryItems_ReturnsOkWithItems()
        {
            // Arrange
            var items = new List<StoryItem>
            {
                new StoryItem { Id = 1, Title = "Test1" },
                new StoryItem { Id = 2, Title = "Test2" }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(items);

            // Act
            var actionResult = await _controller.GetStoryItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(items, okResult.Value);
        }

        [Fact]
        public async Task GetStoryItem_ReturnsOkWhenFound()
        {
            var item = new StoryItem { Id = 1, Title = "Test" };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(item);

            var actionResult = await _controller.GetStoryItem(1);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(item, okResult.Value);
        }

        [Fact]
        public async Task GetStoryItem_ReturnsNotFoundWhenNotFound()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((StoryItem)null);

            var actionResult = await _controller.GetStoryItem(99);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostStoryItem_ReturnsCreatedAtAction()
        {
            var newItem = new StoryItem { Title = "New" };
            var createdItem = new StoryItem { Id = 10, Title = "New" };
            _serviceMock.Setup(s => s.CreateAsync(newItem)).ReturnsAsync(createdItem);

            var actionResult = await _controller.PostStoryItem(newItem);

            var createdAt = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(nameof(StoryItemsController.GetStoryItem), createdAt.ActionName);
            Assert.Equal(createdItem, createdAt.Value);
        }

        [Fact]
        public async Task PutStoryItem_ReturnsBadRequestWhenIdMismatch()
        {
            var item = new StoryItem { Id = 1 };

            var result = await _controller.PutStoryItem(2, item);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutStoryItem_ReturnsNotFoundWhenServiceReturnsFalse()
        {
            var item = new StoryItem { Id = 1 };
            _serviceMock.Setup(s => s.UpdateAsync(item)).ReturnsAsync(false);

            var result = await _controller.PutStoryItem(1, item);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutStoryItem_ReturnsNoContentWhenUpdated()
        {
            var item = new StoryItem { Id = 1 };
            _serviceMock.Setup(s => s.UpdateAsync(item)).ReturnsAsync(true);

            var result = await _controller.PutStoryItem(1, item);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteStoryItem_ReturnsNotFoundWhenServiceReturnsFalse()
        {
            _serviceMock.Setup(s => s.DeleteAsync(5)).ReturnsAsync(false);

            var result = await _controller.DeleteStoryItem(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteStoryItem_ReturnsNoContentWhenDeleted()
        {
            _serviceMock.Setup(s => s.DeleteAsync(5)).ReturnsAsync(true);

            var result = await _controller.DeleteStoryItem(5);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
