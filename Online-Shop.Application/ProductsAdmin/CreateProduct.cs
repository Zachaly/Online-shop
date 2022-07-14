using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Add product to Database
    /// </summary>
    public class CreateProduct
    {
        private readonly IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
            };
            
            await _productManager.AddProduct(product);

            return new Response
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Value = product.Value,
            };
        }

        public class Request
        {
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
