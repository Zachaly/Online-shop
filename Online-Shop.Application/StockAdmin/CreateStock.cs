using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Adds a stock
    /// </summary>
    public class CreateStock
    {
        private readonly IStockManager _stockManager;

        public CreateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var stock = new Stock
            {
                ProductId = request.ProductId,
                Description = request.Description,
                Quantity = request.Quantity
            };

            await _stockManager.AddStock(stock);

            return new Response
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                Description = stock.Description
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }
    }
}
