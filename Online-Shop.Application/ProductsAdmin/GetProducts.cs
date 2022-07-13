﻿using Online_shop.Database;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Gets all products in Database
    /// </summary>
    public class GetProducts
    {
        private AppDbContext _dbContext;

        public GetProducts(AppDbContext dbContext)
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