using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProductToCart(int stockId, int quantity);
        List<CartProduct> GetCartProducts();
        CustomerInformation GetCustomerInformation();
        void AddCustomerInformation(CustomerInformation customerInformation);
        bool RemoveProductFromCart(int stockId, int quantity, bool all = false);
    }
}
