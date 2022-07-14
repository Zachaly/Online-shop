using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Gets all products in Database
    /// </summary>
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Execute()
            => _productManager.GetProducts(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
