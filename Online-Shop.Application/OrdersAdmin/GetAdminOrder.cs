using Microsoft.EntityFrameworkCore;
using Online_shop.DataBase;

namespace Online_Shop.Application.OrdersAdmin
{
    /// <summary>
    /// Gets max ammount of info about order for admin usage
    /// </summary>
    public class GetAdminOrder
    {
        private AppDbContext _dbContext;

        public GetAdminOrder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Response Execute(int id)
            => _dbContext.Orders.Where(order => order.Id == id).
                Include(order => order.OrderStocks).
                ThenInclude(orderStock => orderStock.Stock).
                ThenInclude(stock => stock.Product).
                Select(order => new Response 
                {
                    Id = order.Id,
                    Reference = order.OrderReference,
                    StripeReference = order.StripeReference,

                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Address = order.Address,
                    City = order.City,
                    Email = order.Email,
                    PhoneNumber = order.PhoneNumber,
                    PostCode = order.PostCode,

                    Products = order.OrderStocks.Select(stock => new Product
                    {
                        Name = stock.Stock.Product.Name,
                        Description = stock.Stock.Product.Description,
                        Quantity = stock.Quantity,
                        StockDescription = stock.Stock.Description,
                        Value = stock.Stock.Product.Value
                    })
                }).FirstOrDefault();

        public class Response
        {
            public int Id { get; set; }
            public string StripeReference { get; set; }
            public string Reference { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }

            public IEnumerable<Product> Products { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public string StockDescription { get; set; }
            public decimal Value { get; set; }
            public decimal TotalValue => Value * Quantity;
        }
    }
}
