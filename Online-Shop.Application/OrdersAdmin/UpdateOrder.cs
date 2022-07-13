using Online_shop.Database;

namespace Online_Shop.Application.OrdersAdmin
{
    /// <summary>
    /// Processes an order
    /// </summary>
    public class UpdateOrder
    {
        private AppDbContext _dbContext;

        public UpdateOrder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(order => order.Id == id);

            order.Status += 1;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
