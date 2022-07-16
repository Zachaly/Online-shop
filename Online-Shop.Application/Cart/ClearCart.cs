using Online_Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Cart
{
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
