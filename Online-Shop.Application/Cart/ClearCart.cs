using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Cart
{
    [Service]
    public class ClearCart
    {
        private ISessionManager _sessionManager;

        public ClearCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Execute()
        {
            _sessionManager.ClearCart();
        }
    }
}
