using Microsoft.EntityFrameworkCore;
using Online_shop.DataBase;
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
            => _dbContext.Products.Include(db => db.Stock).Select(product => new ProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Value = $"{product.Value.ToString("N2")}$",
                    StockCount = product.Stock.Sum(stock => stock.Quantity)
                }).ToList();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
        }
    }
}
