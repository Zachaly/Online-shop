using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Gets information about stock
    /// </summary>
    [Service]
    public class GetStock
    {
        private readonly IProductManager _productManager;

        public GetStock(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Execute()
            => _productManager.GetProducsWithStock(product => new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Stock = product.Stock.Select(stock => new StockViewModel
                {
                    Id = stock.Id,
                    Quantity = stock.Quantity,
                    Description = stock.Description
                }).ToList()
            });

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
