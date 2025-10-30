using System;
using System.Threading.Tasks;
using Xunit;
using Moq;

using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Services;
using Catalog.Domain.Entities;

namespace CatalogService.UnitTests.Application
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _service = new ProductService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenProductNameAlreadyExists()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                Name = "Laptop",
                Description = "Laptop de prueba",
                Price = 1500,
                Stock = 10
            };

            _repositoryMock
                .Setup(r => r.ExistsByNameAsync(dto.Name))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(dto));

            // Verify that AddAsync() was never called
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task AddAsync_ShouldAddProduct_WhenNameDoesNotExist()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                Name = "Mouse",
                Description = "Mouse inalámbrico",
                Price = 50,
                Stock = 100
            };

            _repositoryMock
                .Setup(r => r.ExistsByNameAsync(dto.Name))
                .ReturnsAsync(false);

            // Act
            await _service.AddAsync(dto);

            // Assert: verificamos que AddAsync() se llamó exactamente una vez
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
