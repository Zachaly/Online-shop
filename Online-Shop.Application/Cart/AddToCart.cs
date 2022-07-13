using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_shop.DataBase;
using Online_shop.Domain.Models;
using Online_Shop.Application.Infrastructure;
using System.Text;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Adds given stock to a cart, and reduces ammount of currectly avaible stocks
    /// </summary>
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private AppDbContext _dbContext;

        public AddToCart(ISessionManager sessionManager, AppDbContext dbContext)
        {
            _sessionManager = sessionManager;
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {
            var stocksOnHold = _dbContext.StocksOnHold.Where(stock => stock.SessionId == _sessionManager.GetId());
            var stockToHold = _dbContext.Stock.FirstOrDefault(stock => stock.Id == request.StockId);

            if(stockToHold.Quantity < request.Quantity)
            {
                return false;
            }

            if (stocksOnHold.Any(stock => stock.StockId == request.StockId))
            {
                _dbContext.StocksOnHold.FirstOrDefault(stock => stock.StockId == request.StockId).Quantity += request.Quantity;
            }
            else
            {
                _dbContext.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _sessionManager.GetId(),
                    Quantity = request.Quantity,
                    ExpireDate = DateTime.Now.AddMinutes(20)
                });
            }

            stockToHold.Quantity -= request.Quantity;

            foreach(var stock in stocksOnHold)
            {
                stock.ExpireDate = DateTime.Now.AddMinutes(20);
            }

            await _dbContext.SaveChangesAsync();

            _sessionManager.AddProductToCart(request.StockId, request.Quantity);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
