namespace Catalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public Product(string name, string description, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");

            if (quantity > Stock)
                throw new InvalidOperationException("Not enough stock.");

            Stock -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");

            Stock += quantity;
        }
        public void UpdateDetails(string name, string description, decimal price, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }


    }
}
