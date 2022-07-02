using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private AppDbContext _dbContext;

        public UpdateProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ProductViewModel viewModel)
        {

            await _dbContext.SaveChangesAsync();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
