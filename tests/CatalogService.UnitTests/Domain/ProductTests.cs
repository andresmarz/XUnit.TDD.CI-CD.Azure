using Catalog.Domain.Entities;

namespace CatalogService.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void Should_Create_Product_With_Valid_Data()
        {
            var product = new Product("Laptop", "Gaming laptop", 1200m, 10);

            Assert.Equal("Laptop", product.Name);
            Assert.Equal("Gaming laptop", product.Description);
            Assert.Equal(1200m, product.Price);
            Assert.Equal(10, product.Stock);
        }

        [Fact]
        public void Should_Throw_Exception_When_Name_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Product("", "Some description", 1200m, 10)
            );
        }

        [Fact]
        public void Should_Throw_Exception_When_Price_Is_Zero_Or_Negative()
        {
            Assert.Throws<ArgumentException>(() =>
                new Product("Phone", "Smartphone", 0, 10)
            );

            Assert.Throws<ArgumentException>(() =>
                new Product("Phone", "Smartphone", -5, 10)
            );
        }

        [Fact]
        public void Should_Throw_Exception_When_Stock_Is_Negative()
        {
            Assert.Throws<ArgumentException>(() =>
                new Product("Phone", "Smartphone", 100, -1)
            );
        }

        [Fact]
        public void DecreaseStock_Should_Reduce_Stock_Correctly()
        {
            var product = new Product("Phone", "Smartphone", 100, 10);
            product.DecreaseStock(3);

            Assert.Equal(7, product.Stock);
        }

        [Fact]
        public void DecreaseStock_Should_Throw_When_Quantity_Exceeds_Stock()
        {
            var product = new Product("Phone", "Smartphone", 100, 5);
            Assert.Throws<InvalidOperationException>(() => product.DecreaseStock(10));
        }

        [Fact]
        public void IncreaseStock_Should_Add_Stock_Correctly()
        {
            var product = new Product("Phone", "Smartphone", 100, 5);
            product.IncreaseStock(5);

            Assert.Equal(10, product.Stock);
        }
    }
}
