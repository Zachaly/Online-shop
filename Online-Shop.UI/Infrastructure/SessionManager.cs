using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_Shop.Domain.Models;
using Online_Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInformation(CustomerInformation customerInformation)
        {
            var customerInfo = JsonConvert.SerializeObject(customerInformation);

            _session.SetString("customer-info", customerInfo);
        }

        public void AddProductToCart(CartProduct product)
        {
            var cartString = _session.GetString("Cart");
            var cartList = new List<CartProduct>();

            if (!string.IsNullOrEmpty(cartString))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);
            }

            if (cartList.Any(stock => stock.StockId == product.StockId))
            {
                cartList.Find(stock => stock.StockId == product.StockId).Quantity += product.Quantity;
            }
            else
            {
                cartList.Add(product);
            }

            var requestString = JsonConvert.SerializeObject(cartList);

            _session.SetString("Cart", requestString);
        }

        public IEnumerable<TResult> GetCartProducts<TResult>(Func<CartProduct, TResult> selector)
        {
            var cartString = _session.GetString("Cart");

            if (string.IsNullOrEmpty(cartString))
            {
                return Enumerable.Empty<TResult>();
            }

            return JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(cartString).Select(selector);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var customerInfoString = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(customerInfoString))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerInformation>(customerInfoString);
        }

        public string GetId() => _session.Id;

        public void RemoveProductFromCart(int stockId, int quantity)
        {
            var cartString = _session.GetString("Cart");
            var cartList = new List<CartProduct>();

            if (string.IsNullOrEmpty(cartString))
            {
                return;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);

            if (!cartList.Any(stock => stock.StockId == stockId))
            {
                return;
            }

            var product = cartList.Find(stock => stock.StockId == stockId);
            product.Quantity -= quantity;

            if (product.Quantity <= 0)
            {
                cartList.Remove(cartList.First(stock => stock.StockId == stockId));
            }

            var requestString = JsonConvert.SerializeObject(cartList);

            _session.SetString("Cart", requestString);
        }
    }
}
