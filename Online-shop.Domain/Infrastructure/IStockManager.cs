using Online_Shop.Domain.Models;

namespace Online_Shop.Domain.Infrastructure
{
    public interface IStockManager
    {
        Stock GetStockWithProduct(int stockId);
        bool EnoughtStock(int stockId, int quantity);
        Task PutStockOnHold(int stockId, string sessionId, int quantity);
        Task RemoveStockFromHold(int stockId, string sessionId, int quantity);
        Task RemoveStockFromHold(string sessionId);
        Task RefillStocks();
        Task AddStock(Stock stock);
        Task DeleteStockById(int id);
        Task UpdateStocks(IEnumerable<Stock> stocks);
    }
}
