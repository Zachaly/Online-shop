using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Updates information about product
    /// </summary>
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;

        public UpdateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var product = _productManager.GetProductById(request.Id);

            await _productManager.UpdateProduct(product, (prod) =>
            {
                prod.Name = request.Name;
                prod.Description = request.Description;
                prod.Value = request.Value;
            });

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Value = product.Value,
                Description = product.Description,
            };
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
