﻿using Online_shop.DataBase;
using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private AppDbContext _dbContext;

        public CreateStock(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Execute(Request request)
        {

            var stock = new Stock
            {
                ProductId = request.ProductId,
                Description = request.Description,
                Quantity = request.Quantity
            };

            _dbContext.Stock.Add(stock);

            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                Description = stock.Description
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }
    }
}