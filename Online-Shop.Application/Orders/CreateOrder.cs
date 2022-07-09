﻿using Online_shop.DataBase;
using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Orders
{
    public class CreateOrder
    {
        private AppDbContext _dbContext;

        public CreateOrder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Execute(Request request)
        {
            var stocksToUpdate = _dbContext.Stock.AsEnumerable().Where(stock => request.Stocks.Any(x => x.StockId == stock.Id)).ToList();

            foreach(var stock in stocksToUpdate)
            {
                stock.Quantity -= stock.Quantity - request.Stocks.FirstOrDefault(x => x.StockId == stock.Id).Quantity;
            }

            var order = new Order
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                PostCode = request.PostCode,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                StripeReference = request.StripeId,
                OrderStocks = request.Stocks.Select(stock => new OrderStock
                {
                    StockId = stock.StockId,
                    Quantity = stock.Quantity,
                }).ToList(),
                OrderReference = CreateOrderReference()
            };

            _dbContext.Orders.Add(order);


            return await _dbContext.SaveChangesAsync() > 0;
        }

        public string CreateOrderReference()
        {
            var chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789";

            var result = new char[12];
            var random = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] = chars[random.Next(chars.Length)];
            } while (_dbContext.Orders.ToList().Any(order => order.OrderReference == new string(result)));

            return new string(result);
        }

        public class Request
        {
            public string StripeId { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }

            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
