﻿using Online_shop.Database;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Gets product info for admin
    /// </summary>
    public class GetProduct
    {
        private AppDbContext _dbContext;

        public GetProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductViewModel Execute(int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(prod => prod.Id == productId);

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