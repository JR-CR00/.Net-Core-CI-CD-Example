using Microsoft.EntityFrameworkCore;
using TEstApi.Models;
using TEstApi.Repository;
using Xunit;

namespace TEstApi.Tests
{
    public class CategoryRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Fact]
        public void CategoryExists_ById_ReturnsTrue()
        {
            // Arrange
            var dbContext = GetDbContext();
            dbContext.Categories.Add(new Category { Id = 1, Name = "Test Category" });
            dbContext.SaveChanges();
            var repository = new CategoryRepository(dbContext);

            // Act
            var result = repository.CategoryExists(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CreateCategory_AddsCategoryToDb()
        {
            // Arrange
            var dbContext = GetDbContext();
            var repository = new CategoryRepository(dbContext);
            var category = new Category { Name = "New Category" };

            // Act
            var result = repository.CreateCategory(category);

            // Assert
            Assert.True(result);
            Assert.Equal(1, dbContext.Categories.Count());
            Assert.Equal("New Category", dbContext.Categories.First().Name);
        }

        [Fact]
        public void GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var dbContext = GetDbContext();
            dbContext.Categories.AddRange(
                new Category { Id = 1, Name = "Cat 1" },
                new Category { Id = 2, Name = "Cat 2" }
            );
            dbContext.SaveChanges();
            var repository = new CategoryRepository(dbContext);

            // Act
            var result = repository.GetCategories();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
