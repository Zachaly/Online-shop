using Online_shop.DataBase;
using Online_Shop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.ProductsAdmin
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
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Value = product.Value,
                });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
