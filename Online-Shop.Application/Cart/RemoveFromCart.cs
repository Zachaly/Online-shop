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
    public class RemoveFromCart
    {
        private ISession _session;
        private AppDbContext _dbContext;

        public RemoveFromCart(IHttpContextAccessor httpAccessor, AppDbContext dbContext)
        {
            _session = httpAccessor.HttpContext.Session;
            _dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(Request request)
        {
            var cartString = _session.GetString("Cart");
            var cartList = new List<CartProduct>();

            if (string.IsNullOrEmpty(cartString))
            {
                return true;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);

            if(!cartList.Any(stock => stock.StockId == request.StockId))
            {
                return true;
            }

            cartList.Find(stock => stock.StockId == request.StockId).Quantity -= request.Quantity;

            if (request.All)
            {
                cartList.Remove(cartList.First(stock => stock.StockId == request.StockId));
            }

            var requestString = JsonConvert.SerializeObject(cartList);

            _session.SetString("Cart", requestString);

            var stockOnHold = _dbContext.StocksOnHold.FirstOrDefault(stock => stock.StockId == request.StockId && stock.SessionId == _session.Id);

            var stock = _dbContext.Stock.FirstOrDefault(stock => stock.Id == request.StockId);

            if (request.All)
            {
                stock.Quantity += stockOnHold.Quantity;
                stockOnHold.Quantity = 0;
            }
            else
            {
                stock.Quantity += request.Quantity;
                stockOnHold.Quantity -= request.Quantity;
            }

            if(stockOnHold.Quantity <= 0)
            {
                _dbContext.StocksOnHold.Remove(stockOnHold);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; } = false;
        }
    }
}
