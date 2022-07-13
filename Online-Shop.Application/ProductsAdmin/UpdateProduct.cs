using Online_shop.Database;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Updates information about product
    /// </summary>
    public class UpdateProduct
    {
        private AppDbContext _dbContext;

        public UpdateProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var product = _dbContext.Products.FirstOrDefault(prod => prod.Id == request.Id);

            product.Name = request.Name;
            product.Value = request.Value;
            product.Description = request.Description;

            await _dbContext.SaveChangesAsync();
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
