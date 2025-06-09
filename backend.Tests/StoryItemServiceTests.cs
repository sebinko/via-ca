using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace backend.Tests
{
    public class StoryItemServiceTests
    {
        private async Task<(StoryItemService, ApplicationDbContext)> GetServiceAsync(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var context = new ApplicationDbContext(options);
            // Remove any seeded data to avoid duplicate key errors
            context.StoryItems.RemoveRange(context.StoryItems);
            await context.SaveChangesAsync();
            return (new StoryItemService(context), context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsItemsInDescendingOrder()
        {
            var (service, context) = await GetServiceAsync(dbName: Guid.NewGuid().ToString());
            // Arrange
            context.StoryItems.AddRange(
                new StoryItem { Id = 1, Title = "A", Content = "C1", Author = "X", Category = "Cat", CreatedAt = DateTime.UtcNow.AddHours(-1) },
                new StoryItem { Id = 2, Title = "B", Content = "C2", Author = "Y", Category = "Cat", CreatedAt = DateTime.UtcNow }
            );
            await context.SaveChangesAsync();

            // Act
            var items = (await service.GetAllAsync()).ToList();

            // Assert
            Assert.Equal(2, items.Count);
            Assert.Equal(2, items[0].Id);
            Assert.Equal(1, items[1].Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsItemWhenExists()
        {
            var (service, context) = await GetServiceAsync(Guid.NewGuid().ToString());
            var item = new StoryItem { Id = 5, Title = "Test", Content = "C", Author = "A", Category = "Cat", CreatedAt = DateTime.UtcNow };
            context.StoryItems.Add(item);
            await context.SaveChangesAsync();

            var result = await service.GetByIdAsync(5);
            Assert.NotNull(result);
            Assert.Equal(5, result!.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullWhenNotExists()
        {
            var (service, _) = await GetServiceAsync(Guid.NewGuid().ToString());
            var result = await service.GetByIdAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_AddsItemAndSetsCreatedAt()
        {
            var (service, context) = await GetServiceAsync(Guid.NewGuid().ToString());
            var newItem = new StoryItem { Title = "New", Content = "C", Author = "A", Category = "Cat" };

            var created = await service.CreateAsync(newItem);

            Assert.NotEqual(default, created.Id);
            Assert.True((DateTime.UtcNow - created.CreatedAt).TotalSeconds < 5);

            var exists = await context.StoryItems.FindAsync(created.Id);
            Assert.NotNull(exists);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalseWhenItemNotExists()
        {
            var (service, _) = await GetServiceAsync(Guid.NewGuid().ToString());
            var item = new StoryItem { Id = 100, Title = "X", Content = "C", Author = "A", Category = "Cat", CreatedAt = DateTime.UtcNow };

            var result = await service.UpdateAsync(item);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrueAndUpdatesItem()
        {
            var (service, context) = await GetServiceAsync(Guid.NewGuid().ToString());
            var item = new StoryItem { Id = 1, Title = "Old", Content = "C", Author = "A", Category = "Cat", CreatedAt = DateTime.UtcNow };
            context.StoryItems.Add(item);
            await context.SaveChangesAsync();

            item.Title = "Updated";
            var result = await service.UpdateAsync(item);

            Assert.True(result);
            var updated = await context.StoryItems.FindAsync(1);
            Assert.Equal("Updated", updated!.Title);
            // CreatedAt should be unchanged
            Assert.Equal(item.CreatedAt, updated.CreatedAt);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalseWhenItemNotExists()
        {
            var (service, _) = await GetServiceAsync(Guid.NewGuid().ToString());
            var result = await service.DeleteAsync(123);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrueAndRemovesItem()
        {
            var (service, context) = await GetServiceAsync(Guid.NewGuid().ToString());
            var item = new StoryItem { Id = 2, Title = "T", Content = "C", Author = "A", Category = "Cat", CreatedAt = DateTime.UtcNow };
            context.StoryItems.Add(item);
            await context.SaveChangesAsync();

            var result = await service.DeleteAsync(2);

            Assert.True(result);
            var exists = await context.StoryItems.FindAsync(2);
            Assert.Null(exists);
        }
    }
}
