using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.OrdersAdmin
{
    /// <summary>
    /// Processes an order
    /// </summary>
    [Service]
    public class UpdateOrder
    {
        private readonly IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public async Task<bool> ExecuteAsync(int id) => await _orderManager.AdvanceOrder(id);
    }
}
