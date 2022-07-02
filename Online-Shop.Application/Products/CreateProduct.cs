using Online_shop.DataBase;
using Online_shop.Domain.Models;
using Online_Shop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Products
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
                Value = decimal.Parse(viewModel.Value),
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
