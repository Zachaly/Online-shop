using Microsoft.AspNetCore.Http;
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
    public class AddToCart
    {
        private ISession _session;
        private AppDbContext _dbContext;

        public AddToCart(IHttpContextAccessor httpAccessor, AppDbContext dbContext)
        {
            _session = httpAccessor.HttpContext.Session;
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {
            var stocksOnHold = _dbContext.StocksOnHold.Where(stock => stock.SessionId == _session.Id);
            var stockToHold = _dbContext.Stock.FirstOrDefault(stock => stock.Id == request.StockId);

            if(stockToHold.Quantity < request.Quantity)
            {
                return false;
            }

            if (stocksOnHold.Any(stock => stock.StockId == request.StockId))
            {
                _dbContext.StocksOnHold.FirstOrDefault(stock => stock.StockId == request.StockId).Quantity += request.Quantity;
            }
            else
            {
                _dbContext.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _session.Id,
                    Quantity = request.Quantity,
                    ExpireDate = DateTime.Now.AddMinutes(20)
                });
            }

            stockToHold.Quantity += request.Quantity;

            foreach(var stock in stocksOnHold)
            {
                stock.ExpireDate = DateTime.Now.AddMinutes(20);
            }

            await _dbContext.SaveChangesAsync();

            var cartString = _session.GetString("Cart");
            var cartList = new List<CartProduct>();

            if (!string.IsNullOrEmpty(cartString))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);
            }

            if(cartList.Any(stock => stock.StockId == request.StockId))
            {
                cartList.Find(stock => stock.StockId == request.StockId).Quantity += request.Quantity;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    Quantity = request.Quantity,
                    StockId = request.StockId
                });
            }

            var requestString = JsonConvert.SerializeObject(cartList);

            _session.SetString("Cart", requestString);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
