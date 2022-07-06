using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

        public AddToCart(ISession session)
        {
            _session = session;
        }

        public void Execute(Request request)
        {
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
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
