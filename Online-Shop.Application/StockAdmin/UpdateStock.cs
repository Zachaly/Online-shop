using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Updates info about stock
    /// </summary>
    public class UpdateStock
    {
        private readonly IStockManager _stockManager;

        public UpdateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var stocks = new List<Stock>();

            foreach(var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Quantity = stock.Quantity,
                    Description = stock.Description,
                    ProductId = stock.ProductId,
                });
            }
            
            await _stockManager.UpdateStocks(stocks);

            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
