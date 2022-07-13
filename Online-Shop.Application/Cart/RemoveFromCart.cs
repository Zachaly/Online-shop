using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_shop.Database;
using Online_shop.Domain.Infrastructure;
using Online_shop.Domain.Models;
using Online_Shop.Domain.Infrastructure;
using System.Text;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Removes products from cart and releases a stock on hold
    /// </summary>
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public RemoveFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {
            if (request.Quantity < 0)
                return false;

            await _stockManager.RemoveStockFromHold(request.StockId, _sessionManager.GetId(), request.Quantity);

            _sessionManager.RemoveProductFromCart(request.StockId, request.Quantity);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; } = false;
        }
    }
}
