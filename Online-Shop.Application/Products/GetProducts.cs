using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Products
{
    /// <summary>
    /// Gets products avaible for customer
    /// </summary>
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Execute()
            => _productManager.GetProducsWithStock(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value.GetPriceString(),
                StockCount = product.Stock.Sum(stock => stock.Quantity)
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
        }
    }
}
