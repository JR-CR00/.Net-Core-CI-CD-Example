using Microsoft.EntityFrameworkCore;
using TEstApi.Models;
using TEstApi.Repository;
using Xunit;

namespace TEstApi.Tests
{
    public class ProductRepositoryTests
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
        public void CreateProduct_AddsProductToDb()
        {
            // Arrange
            var dbContext = GetDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product 
            { 
                Name = "Test Product", 
                Price = 100, 
                SKU = "TEST-SKU",
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Test Category" }
            };

            // Act
            var result = repository.CreateProduct(product);

            // Assert
            Assert.True(result);
            Assert.Equal(1, dbContext.Products.Count());
            Assert.Equal("Test Product", dbContext.Products.First().Name);
        }

        [Fact]
        public void BuyProduct_DecreasesStock()
        {
            // Arrange
            var dbContext = GetDbContext();
            var product = new Product 
            { 
                Id = 1,
                Name = "Test Product", 
                Stock = 10,
                Price = 100, 
                SKU = "TEST-SKU",
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Test Category" }
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var repository = new ProductRepository(dbContext);

            // Act
            var result = repository.BuyProduct("Test Product", 3);

            // Assert
            Assert.True(result);
            Assert.Equal(7, dbContext.Products.Find(1).Stock);
        }

        [Fact]
        public void GetProductPaginated_ReturnsCorrectPage()
        {
            // Arrange
            var dbContext = GetDbContext();
            var category = new Category { Id = 1, Name = "Test Category" };
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product 
                { 
                    Id = i, 
                    Name = $"Product {i}",
                    Price = 10,
                    SKU = $"SKU-{i}",
                    CategoryId = 1,
                    Category = category
                });
            }
            dbContext.SaveChanges();
            var repository = new ProductRepository(dbContext);

            // Act
            var result = repository.GetProductPaginated(2, 3); // Page 2, Size 3

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(4, result.First().Id); // Products 4, 5, 6
        }
    }
}
