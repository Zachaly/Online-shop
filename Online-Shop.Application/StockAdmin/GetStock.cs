using Online_shop.Database;
using Microsoft.EntityFrameworkCore;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Gets information about stock
    /// </summary>
    public class GetStock
    {
        private AppDbContext _dbContext;

        public GetStock(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductViewModel> Execute()
            => _dbContext.Products.Include(product => product.Stock).
            Select(product => new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Stock = product.Stock.Select(stock => new StockViewModel
                    {
                        Id = stock.Id,
                        Quantity = stock.Quantity,
                        Description = stock.Description
                    }).ToList()
            }).ToList();

        public class StockViewModel
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
