using Online_shop.Database;
using Online_shop.Domain.Enums;

namespace Online_Shop.Application.OrdersAdmin
{
    /// <summary>
    /// Gets all orders
    /// </summary>
    public class GetOrders
    {
        private AppDbContext _dbContext;

        public GetOrders(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderModel> Execute(int status) 
            => _dbContext.Orders.Where(order => order.Status == (OrderStatus)status).
                Select(order => new OrderModel
                {
                    Id = order.Id,
                    OrderEmail = order.Email,
                    OrderReference = order.OrderReference
                }).ToList();

        public class OrderModel
        {
            public int Id { get; set; }
            public string OrderReference { get; set; }
            public string OrderEmail { get; set; }
        }
    }
}
