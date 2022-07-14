using Online_Shop.Domain.Enums;
using Online_Shop.Domain.Models;

namespace Online_Shop.Domain.Infrastructure
{
    public interface IOrderManager
    {
        Task<bool> CreateOrder(Order order);
        bool DoesOrderReferenceExist(string reference);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        IEnumerable<T> GetOrdersByStatus<T>(OrderStatus status, Func<Order, T> selector);
        Task<bool> AdvanceOrder(int id);
    }
}
