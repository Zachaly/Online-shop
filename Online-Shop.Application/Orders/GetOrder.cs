using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Orders
{
    /// <summary>
    /// Get order basing on reference
    /// </summary>
    [Service]
    public class GetOrder
    {
        private readonly IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Response Execute(string reference)
            => _orderManager.GetOrderByReference(reference, order => new Response
            {
                FirstName = order.FirstName,
                LastName = order.LastName,
                Address = order.Address,
                City = order.City,
                PostCode = order.PostCode,
                Email = order.Email,
                OrderReference = order.OrderReference,
                PhoneNumber = order.PhoneNumber,
                Products = order.OrderStocks.Select(stock => new Product
                {
                    Description = stock.Stock.Product.Description,
                    Name = stock.Stock.Product.Name,
                    Quantity = stock.Quantity,
                    StockDescription = stock.Stock.Description,
                    Value = stock.Stock.Product.Value.ToString("N2")
                }),
                TotalValue = order.OrderStocks.Sum(stock => stock.Stock.Product.Value).ToString("N2")
            });
        
        public class Response
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string OrderReference { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }

            public IEnumerable<Product> Products { get; set; }
            public string TotalValue { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int Quantity { get; set; }
            public string StockDescription { get; set; }
        }
    }
}
