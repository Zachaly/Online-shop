using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Online_shop.DataBase;
using Online_shop.Domain.Models;
using Online_Shop.Application.Infrastructure;
using System.Text;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Gets products in cart
    /// </summary>
    public class GetCart
    {
        private ISessionManager _sessionManager;
        private AppDbContext _dbContext;

        public GetCart(ISessionManager sessionManager, AppDbContext dbContext)
        {
            _sessionManager = sessionManager;
            _dbContext = dbContext;
        }

        public IEnumerable<Response> Execute()
        {
            var cartList = _sessionManager.GetCartProducts();

            if(cartList is null)
            {
                return Enumerable.Empty<Response>();
            }

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
