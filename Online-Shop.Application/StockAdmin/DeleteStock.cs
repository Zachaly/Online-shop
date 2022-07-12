using Online_shop.DataBase;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Removes stock from database
    /// </summary>
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
