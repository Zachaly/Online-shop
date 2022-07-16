using Online_Shop.Domain.Enums;
using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.OrdersAdmin
{
    /// <summary>
    /// Gets all orders
    /// </summary>
    [Service]
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IEnumerable<OrderModel> Execute(int status)
            => _orderManager.GetOrdersByStatus((OrderStatus)status, order => new OrderModel
            {
                Id = order.Id,
                OrderEmail = order.Email,
                OrderReference = order.OrderReference
            });

        public class OrderModel
        {
            public int Id { get; set; }
            public string OrderReference { get; set; }
            public string OrderEmail { get; set; }
        }
    }
}
