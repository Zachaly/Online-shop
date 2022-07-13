﻿using Online_shop.Database;
using Online_shop.Domain.Models;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Add product to Database
    /// </summary>
    public class CreateProduct
    {
        private AppDbContext _dbContext;

        public CreateProduct(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
            };
            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

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
