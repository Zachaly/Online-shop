using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Application.Orders
{
    /// <summary>
    /// Creates an order
    /// </summary>
    [Service]
    public class CreateOrder
    {
        private readonly IOrderManager _orderManager;
        private readonly IStockManager _stockManager;

        public CreateOrder(IOrderManager orderManager, IStockManager stockManager)
        {
            _orderManager = orderManager;
            _stockManager = stockManager;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {
            var order = new Order
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                PostCode = request.PostCode,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                StripeReference = request.StripeId,
                OrderStocks = request.Stocks.Select(stock => new OrderStock
                {
                    StockId = stock.StockId,
                    Quantity = stock.Quantity,
                }).ToList(),
                OrderReference = CreateOrderReference()
            };

            var success = await _orderManager.CreateOrder(order);

            if (success)
            {
                await _stockManager.RemoveStockFromHold(request.SessionId);
            }

            return success;
        }

        public string CreateOrderReference()
        {
            var chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789";

            var result = new char[12];
            var random = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] = chars[random.Next(chars.Length)];
            } while (_orderManager.DoesOrderReferenceExist(new string(result)));

            return new string(result);
        }

        public class Request
        {
            public string StripeId { get; set; }
            public string SessionId { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }

            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
