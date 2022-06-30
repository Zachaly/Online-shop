using Online_shop.DataBase;
using Online_shop.Domain.Models;
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

        public async Task Do(string name, string description, decimal value)
        {
            _dbContext.Products.Add(new Product
            {
                Name = name,
                Description = description,
                Value = value
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
