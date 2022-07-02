using Online_shop.DataBase;
using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private AppDbContext _dbContext;

        public CreateProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ProductViewModel viewModel)
        {
            _dbContext.Products.Add(new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Value = viewModel.Value,
            });

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
