using Xunit;
using Catalog.Domain.Entities;
using System;

namespace CatalogService.UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Product_Should_Be_Created_With_Valid_Values()
        {
            // Arrange
            var name = "Laptop";
            var description = "Gaming Laptop";
            var price = 1500m;
            var stock = 10;

            // Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                Stock = stock
            };

            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
            Assert.NotEqual(Guid.Empty, product.Id);
        }

        [Fact]
        public void Product_Should_Allow_Updating_Stock()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Description = "Gaming Laptop",
                Price = 1500m,
                Stock = 5
            };

            // Act
            product.Stock = 8;

            // Assert
            Assert.Equal(8, product.Stock);
        }
    }
}
