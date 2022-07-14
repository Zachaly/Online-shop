using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Adds given stock to a cart, and reduces ammount of currectly avaible stocks
    /// </summary>
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public AddToCart(ISessionManager sessionManager,IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {

            if(!_stockManager.EnoughtStock(request.StockId, request.Quantity))
            {
                return false;
            }

            await _stockManager.PutStockOnHold(request.StockId, _sessionManager.GetId(), request.Quantity);

            var stock = _stockManager.GetStockWithProduct(request.StockId);

            var cartProduct = new CartProduct
            {
                ProductId = stock.ProductId,
                StockId = stock.Id,
                ProductName = stock.Product.Name,
                Quantity = request.Quantity,
                Value = stock.Product.Value
            };

            _sessionManager.AddProductToCart(cartProduct);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
