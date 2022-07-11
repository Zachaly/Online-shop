using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Online_shop.DataBase;
using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.Cart
{
    public class GetCart
    {
        private ISession _session;
        private AppDbContext _dbContext;

        public GetCart(IHttpContextAccessor session, AppDbContext dbContext)
        {
            _session = session.HttpContext.Session;
            _dbContext = dbContext;
        }

        public IEnumerable<Response> Execute()
        {
            var cartString = _session.GetString("Cart");

            if (string.IsNullOrEmpty(cartString))
            {
                return Enumerable.Empty<Response>();
            }

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);

            // added AsEnumerable() because without it Linq goes nuts and throws an error
            var response = _dbContext.Stock.Include(stock => stock.Product).AsEnumerable().
                Where(stock => cartList.Any(prod => prod.StockId == stock.Id)).
                Select(stock => new Response
                {
                    Name = stock.Product.Name,
                    Quantity = cartList.FirstOrDefault(prod => prod.StockId == stock.Id).Quantity,
                    StockId = stock.Id,
                    Value = $"{stock.Product.Value.ToString("N2")}$",
                    RealValue = stock.Product.Value
                }).ToList();

            return response;
        }

        public class Response
        {
            public string Name { set; get; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
