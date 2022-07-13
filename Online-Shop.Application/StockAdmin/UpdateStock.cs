﻿using Online_shop.Database;
using Online_shop.Domain.Models;

namespace Online_Shop.Application.StockAdmin
{
    /// <summary>
    /// Updates info about stock
    /// </summary>
    public class UpdateStock
    {
        private AppDbContext _dbContext;

        public UpdateStock(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var stocks = new List<Stock>();

            foreach(var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Quantity = stock.Quantity,
                    Description = stock.Description,
                    ProductId = stock.ProductId,
                });
            }
            
            _dbContext.UpdateRange(stocks);

            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
