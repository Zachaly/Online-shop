using Online_shop.DataBase;
using Online_Shop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Products
{
    public class GetProducts
    {
        private AppDbContext _dbContext;

        public GetProducts(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductViewModel> Execute()
            => _dbContext.Products.ToList().Select(product => new ProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Value = $"{product.Value.ToString("N2")}$",
                });
    }
}
