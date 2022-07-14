using Microsoft.EntityFrameworkCore;
using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Database
{
    public class StockManager : IStockManager
    {
        private AppDbContext _dbContext;

        public StockManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddStock(Stock stock)
        {
            _dbContext.Stock.Add(stock);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteStockById(int id)
        {
            var stock = _dbContext.Stock.FirstOrDefault(stock => stock.Id == id);

            _dbContext.Stock.Remove(stock);

            return _dbContext.SaveChangesAsync();
        }

        public bool EnoughtStock(int stockId, int quantity)
            => _dbContext.Stock.FirstOrDefault(stock => stock.Id == stockId).Quantity >= quantity;

        public Stock GetStockWithProduct(int stockId)
            => _dbContext.Stock.Include(stock => stock.Product).FirstOrDefault(stock => stock.Id == stockId);

        public async Task PutStockOnHold(int stockId, string sessionId, int quantity)
        {
            _dbContext.Stock.FirstOrDefault(stock => stock.Id == stockId).Quantity -= quantity;

            var stocksOnHold = _dbContext.StocksOnHold.Where(stock => stock.SessionId == sessionId);
            if (stocksOnHold.Any(stock => stock.StockId == stockId))
            {
                stocksOnHold.FirstOrDefault(stock => stock.StockId == stockId).Quantity += quantity;
            }
            else
            {
                _dbContext.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Quantity = quantity,
                    ExpireDate = DateTime.Now.AddMinutes(20)
                });
            }

            foreach (var stock in stocksOnHold)
            {
                stock.ExpireDate = DateTime.Now.AddMinutes(20);
            }

            await _dbContext.SaveChangesAsync();
        }

        public Task RefillStocks()
        {
            var stocksOnHold = _dbContext.StocksOnHold.Where(stock => stock.ExpireDate < DateTime.Now).ToList();

            if (stocksOnHold.Count > 0)
            {
                var stockToRefill = _dbContext.Stock.AsEnumerable().
                    Where(stock => stocksOnHold.Any(x => x.StockId == stock.Id)).ToList();

                foreach (var stock in stockToRefill)
                {
                    stock.Quantity += stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Quantity;
                }

                _dbContext.RemoveRange(stocksOnHold);

                return _dbContext.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public Task RemoveStockFromHold(int stockId, string sessionId, int quantity)
        {
            var stockOnHold = _dbContext.StocksOnHold.
            FirstOrDefault(stock => stock.StockId == stockId
            && stock.SessionId == sessionId);

            var stock = _dbContext.Stock.FirstOrDefault(stock => stock.Id == stockId);

            stock.Quantity += quantity;
            stockOnHold.Quantity -= quantity;

            if (stockOnHold.Quantity <= 0)
            {
                _dbContext.Remove(stockOnHold);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stocksOnHold = _dbContext.StocksOnHold.Where(stock => stock.SessionId == sessionId).ToList();

            _dbContext.RemoveRange(stocksOnHold);

            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateStocks(IEnumerable<Stock> stocks)
        {
            _dbContext.UpdateRange(stocks);

            return _dbContext.SaveChangesAsync();
        }
    }
}
