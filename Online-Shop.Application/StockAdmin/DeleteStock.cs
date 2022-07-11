using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private AppDbContext _dbContext;

        public DeleteStock(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(int id)
        {

            var stock = _dbContext.Stock.FirstOrDefault(stock => stock.Id == id);

            _dbContext.Stock.Remove(stock);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
