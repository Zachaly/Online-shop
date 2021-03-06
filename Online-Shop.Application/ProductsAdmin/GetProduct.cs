using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Gets product info for admin
    /// </summary>
    [Service]
    public class GetProduct
    {
        private readonly IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public ProductViewModel Execute(int productId)
        {
            var product = _productManager.GetProductById(productId);

            return new ProductViewModel 
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
