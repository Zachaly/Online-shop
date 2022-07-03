using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private AppDbContext _dbContext;

        public GetProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductViewModel Execute(int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(prod => prod.Id == productId);

            return new ProductViewModel 
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }


        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
