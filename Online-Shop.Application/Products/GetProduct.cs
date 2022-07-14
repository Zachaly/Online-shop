using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Products
{
    /// <summary>
    /// Gets product info for customer
    /// </summary>
    public class GetProduct
    {
        private readonly IProductManager _productManager;
        private readonly IStockManager _stockManager;

        public GetProduct(IProductManager productManager, IStockManager stockManager)
        {
            _productManager = productManager;
            _stockManager = stockManager;
        }

        public async Task<ProductViewModel> ExecuteAsync(string name)
        {
            await _stockManager.RefillStocks();

            return _productManager.GetProductByName(name, product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value.GetPriceString(),
                Stock = product.Stock.Select(stock => new StockViewModel
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Quantity = stock.Quantity,
                })
            });
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
