using Online_Shop.Domain.Enums;

namespace Online_Shop.Database.Tests
{
    public abstract class DbTest : IDisposable
    {
        protected AppDbContext _dbContext;

        public DbTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new AppDbContext(options.Options);

            // test orders
            AddTestOrders();
            AddTestProducts();
            AddTestStocks();

            _dbContext.SaveChanges();
        }

        private void AddTestOrders()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Address = "mickiewicza 49",
                    City = "warszawa",
                    FirstName = "jarosław",
                    LastName = "kaczyński",
                    Email = "jaroslaw.kaczynski@sejm.pl",
                    OrderReference = "ref1",
                    PhoneNumber = "123456789",
                    PostCode = "01-650",
                    Status = OrderStatus.Pending,
                    StripeReference = "xyz"
                },
                new Order
                {
                    Id = 2,
                    Address = "krakowska 40",
                    City = "beblo",
                    FirstName = "stefan",
                    LastName = "trojan",
                    Email = "stefcio@vatican.ru",
                    OrderReference = "ref2",
                    PhoneNumber = "666666666",
                    PostCode = "32-089",
                    Status = OrderStatus.Packed,
                    StripeReference = "xyz"
                },
                new Order
                {
                    Id = 3,
                    Address = "zachodnia 58",
                    City = "beblo",
                    FirstName = "zachały",
                    LastName = "soja",
                    Email = "zachary@gmail.com",
                    OrderReference = "ref3",
                    PhoneNumber = "123456789",
                    PostCode = "32-089",
                    Status = OrderStatus.Shipped,
                    StripeReference = "xyz"
                }
            };

            _dbContext.Orders.AddRange(orders);
        }

        private void AddTestProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Description = "Description 1",
                    Name = "Product 1",
                    Value = 1
                },
                new Product
                {
                    Id = 2,
                    Description = "Description 2",
                    Name = "Product 2",
                    Value = 2
                },
                new Product
                {
                    Id = 3,
                    Description = "Description 3",
                    Name = "Product 3",
                    Value = 3
                }
            };

            _dbContext.Products.AddRange(products);
        }

        private void AddTestStocks()
        {
            var stocks = new List<Stock>
            {
                new Stock
                {
                    Id = 1,
                    Description = "product 1 - stock 1",
                    ProductId = 1,
                    Quantity = 10,
                },
                new Stock
                {
                    Id = 2,
                    Description = "product 1 - stock 2",
                    ProductId = 1,
                    Quantity = 20,
                },
                new Stock
                {
                    Id = 3,
                    Description = "product 2 - stock 1",
                    ProductId = 2,
                    Quantity = 30,
                },
                new Stock
                {
                    Id = 4,
                    Description = "product 3 - stock 1",
                    ProductId = 2,
                    Quantity = 40,
                },
                new Stock
                {
                    Id = 5,
                    Description = "product 3 - stock 2",
                    ProductId = 3,
                    Quantity = 50,
                },
                new Stock
                {
                    Id = 6,
                    Description = "product 3 - stock 3",
                    ProductId = 3,
                    Quantity = 60,
                },
            };

            _dbContext.AddRange(stocks);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
