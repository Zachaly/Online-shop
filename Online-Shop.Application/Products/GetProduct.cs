using Microsoft.EntityFrameworkCore;
using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Products
{
    public class GetProduct
    {
        private AppDbContext _dbContext;

        public GetProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductViewModel Execute(string name)
            => _dbContext.Products.Include(ctx => ctx.Stock).
            Where(product => product.Name == name).
            Select(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Value = $"{product.Value.ToString("N2")}$",
                Stock = product.Stock.Select(stock => new StockViewModel
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    InStock = stock.Quantity > 0,
                })
            }).FirstOrDefault();


        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}
