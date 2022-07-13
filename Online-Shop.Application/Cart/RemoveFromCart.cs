using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_shop.DataBase;
using Online_shop.Domain.Models;
using Online_Shop.Application.Infrastructure;
using System.Text;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Removes products from cart and releases a stock on hold
    /// </summary>
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private AppDbContext _dbContext;

        public RemoveFromCart(ISessionManager sessionManager, AppDbContext dbContext)
        {
            _sessionManager = sessionManager;
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {
            var complete = _sessionManager.RemoveProductFromCart(request.StockId, request.Quantity, request.All);

            if (complete)
            {
                return true;
            }

            var stockOnHold = _dbContext.StocksOnHold.
                FirstOrDefault(stock => stock.StockId == request.StockId 
                && stock.SessionId == _sessionManager.GetId());

            var stock = _dbContext.Stock.FirstOrDefault(stock => stock.Id == request.StockId);

            if (request.All)
            {
                stock.Quantity += stockOnHold.Quantity;
                stockOnHold.Quantity = 0;
            }
            else
            {
                stock.Quantity += request.Quantity;
                stockOnHold.Quantity -= request.Quantity;
            }

            if(stockOnHold.Quantity <= 0)
            {
                _dbContext.StocksOnHold.Remove(stockOnHold);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; } = false;
        }
    }
}
