using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Removes products from cart and releases a stock on hold
    /// </summary>
    [Service]
    public class RemoveFromCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

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
