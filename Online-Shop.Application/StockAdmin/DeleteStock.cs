using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Removes stock from Database
    /// </summary>
    [Service]
    public class DeleteStock
    {
        private readonly IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            await _stockManager.DeleteStockById(id);
            return true;
        }
    }
}
