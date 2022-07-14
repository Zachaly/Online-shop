using Online_Shop.Domain.Models;

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
