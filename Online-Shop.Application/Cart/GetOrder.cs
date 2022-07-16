using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Gets information from session needed in creating an order
    /// </summary>
    [Service]
    public class GetOrder
    {
        private readonly ISessionManager _sessionManager;

        public GetOrder(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Response Execute()
        {
            var productList = _sessionManager.GetCartProducts(product => new Product
            {
                Name = product.ProductName,
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                StockId = product.StockId,
                Value = (int)(product.Value * 100),
            }).ToList();

            var customerInfo = _sessionManager.GetCustomerInformation();

            return new Response
            {
                Products = productList,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInfo.FirstName,
                    Address = customerInfo.Address,
                    City = customerInfo.City,
                    Email = customerInfo.Email,
                    LastName = customerInfo.LastName,
                    PhoneNumber = customerInfo.PhoneNumber,
                    PostCode = customerInfo.PostCode
                }
            };
        }

        public class Product
        {
            public int ProductId { get; set; }
            public string Name { set; get; }
            public int Value { get; set; }
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public class Response
        {
            public ICollection<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int TotalCharge => Products.Sum(prod => prod.Value * prod.Quantity); 
        }

        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
