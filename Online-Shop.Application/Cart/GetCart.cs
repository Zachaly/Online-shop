using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Gets products in cart
    /// </summary>
    public class GetCart
    {
        private readonly ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IEnumerable<Response> Execute()
            => _sessionManager.GetCartProducts(item => new Response
                {
                    Name = item.ProductName,
                    Quantity = item.Quantity,
                    StockId = item.StockId,
                    Value = item.Value.GetPriceString(),
                    RealValue = item.Value
                });

        public class Response
        {
            public string Name { set; get; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
