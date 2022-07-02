using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private AppDbContext _dbContext;

        public DeleteProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(int productId)
        {
            _dbContext.Products.Remove(_dbContext.Products.FirstOrDefault(product => product.Id == productId));

            await _dbContext.SaveChangesAsync();
        }
    }
}
