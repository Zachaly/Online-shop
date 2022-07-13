using Microsoft.EntityFrameworkCore;
using Online_shop.Database;
using Online_shop.Domain.Infrastructure;
using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Database
{
    public class StockManager : IStockManager
    {
        private AppDbContext _dbContext;

        public StockManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
