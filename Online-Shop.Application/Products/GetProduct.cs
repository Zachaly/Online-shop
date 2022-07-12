using Microsoft.EntityFrameworkCore;
using Online_shop.DataBase;

namespace Online_Shop.Application.Products
{
    /// <summary>
    /// Gets product info for customer
    /// </summary>
    public class GetProduct
    {
        private AppDbContext _dbContext;

        public GetProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductViewModel> ExecuteAsync(string name)
        {
            var stocksOnHold = _dbContext.StocksOnHold.Where(stock => stock.ExpireDate < DateTime.Now).ToList();

            if(stocksOnHold.Count > 0)
            {
                var stockToRefill = _dbContext.Stock.AsEnumerable().
                    Where(stock => stocksOnHold.Any(x => x.StockId == stock.Id)).ToList();

                foreach(var stock in stockToRefill)
                {
                    stock.Quantity += stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Quantity;
                }

                _dbContext.RemoveRange(stocksOnHold);

                await _dbContext.SaveChangesAsync();
            }

            return _dbContext.Products.Include(ctx => ctx.Stock).
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
                        Quantity = stock.Quantity,
                    })
                }).FirstOrDefault();
        }


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
            public int Quantity { get; set; }
        }
    }
}
