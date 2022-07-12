using Online_shop.DataBase;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Removes product from database
    /// </summary>
    public class DeleteProduct
    {
        private AppDbContext _dbContext;

        public DeleteProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(int productId)
        {
            _dbContext.Products.Remove(_dbContext.Products.FirstOrDefault(product => product.Id == productId));

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
