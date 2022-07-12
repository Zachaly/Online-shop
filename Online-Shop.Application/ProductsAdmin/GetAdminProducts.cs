using Online_shop.DataBase;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Gets all products in database
    /// </summary>
    public class GetAdminProducts
    {
        private AppDbContext _dbContext;

        public GetAdminProducts(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductViewModel> Execute()
            => _dbContext.Products.ToList().Select(product => new ProductViewModel
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
