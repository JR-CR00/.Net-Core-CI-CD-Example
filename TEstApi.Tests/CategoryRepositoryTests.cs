using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TEstApi.Models;
using TEstApi.Repository;
using Xunit;

namespace TEstApi.Tests
{
    public class CategoryRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            }

            var databaseContext = new ApplicationDbContext(optionsBuilder.Options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Fact]
        public void CategoryExists_ById_ReturnsTrue()
        {
            // Arrange
            var dbContext = GetDbContext();
            var existingCategory = dbContext.Categories.FirstOrDefault(c => c.Id == 1);
            if (existingCategory == null)
            {
                dbContext.Categories.Add(new Category { Id = 1, Name = "Test Category" });
                dbContext.SaveChanges();
            }
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
            var categoryName = "New Category " + Guid.NewGuid().ToString();
            var category = new Category { Name = categoryName };

            // Act
            var result = repository.CreateCategory(category);

            // Assert
            Assert.True(result);
            Assert.Contains(dbContext.Categories, c => c.Name == categoryName);
        }

        [Fact]
        public void GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var dbContext = GetDbContext();
            // Limpiamos o aseguramos datos mínimos para el test
            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(
                    new Category { Name = "Cat 1" },
                    new Category { Name = "Cat 2" }
                );
                dbContext.SaveChanges();
            }
            var repository = new CategoryRepository(dbContext);

            // Act
            var result = repository.GetCategories();

            // Assert
            Assert.True(result.Count >= 1);
        }
    }
}
