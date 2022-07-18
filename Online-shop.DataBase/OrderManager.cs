using Microsoft.EntityFrameworkCore;
using Online_Shop.Domain.Enums;
using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Database
{
    public class OrderManager : IOrderManager
    {
        private AppDbContext _dbContext;

        public OrderManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            _dbContext.Orders.Add(order);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool DoesOrderReferenceExist(string reference)
            => _dbContext.Orders.AsEnumerable().Any(order => order.OrderReference == reference);

        private TResult GetOrder<TResult>(Func<Order, bool> condition, Func<Order, TResult> selector)
        => _dbContext.Orders.
                Include(order => order.OrderStocks).
                ThenInclude(order => order.Stock).
                ThenInclude(stock => stock.Product).
                AsEnumerable().
                Where(order => condition(order)).
                Select(selector).
                FirstOrDefault();

        public TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector)
            => GetOrder(order => order.OrderReference == reference, selector);

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
            => GetOrder(order => order.Id == id, selector);

        public IEnumerable<T> GetOrdersByStatus<T>(OrderStatus status, Func<Order, T> selector)
            => _dbContext.Orders.
                Where(order => order.Status == status).
                Select(selector).ToList();

        public async Task<bool> AdvanceOrder(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(order => order.Id == id);

            if((int)order.Status <= 1)
            {
                order.Status++;
                return await _dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
