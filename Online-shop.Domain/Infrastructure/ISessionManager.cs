using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Domain.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProductToCart(CartProduct product);
        IEnumerable<TResult> GetCartProducts<TResult>(Func<CartProduct, TResult> selector);
        CustomerInformation GetCustomerInformation();
        void AddCustomerInformation(CustomerInformation customerInformation);
        void RemoveProductFromCart(int stockId, int quantity);
    }
}
