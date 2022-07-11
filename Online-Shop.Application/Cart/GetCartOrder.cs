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
    public class GetCartOrder
    {
        private ISession _session;
        private AppDbContext _dbContext;

        public GetCartOrder(IHttpContextAccessor session, AppDbContext dbContext)
        {
            _session = session.HttpContext.Session;
            _dbContext = dbContext;
        }

        public Response Execute()
        {
            var cartString = _session.GetString("Cart");

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);

            var productList = _dbContext.Stock.Include(x => x.Product).AsEnumerable().
                Where(stock => cartList.Any(item => item.StockId == stock.Id)).
                Select(stock => new Product
                {
                    Name = stock.Product.Name,
                    ProductId = stock.ProductId,
                    Quantity = cartList.FirstOrDefault(x => x.StockId == stock.Id).Quantity,
                    StockId = stock.Id,
                    Value = (int)(stock.Product.Value * 100),
                }).ToList();

            var customerInfoString = _session.GetString("customer-info");

            var customerInfo = JsonConvert.DeserializeObject<Online_shop.Domain.Models.CustomerInformation>(customerInfoString);

            return new Response
            {
                Products = productList,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInfo.FirstName,
                    Address = customerInfo.Address,
                    City = customerInfo.City,
                    Email = customerInfo.Email,
                    LastName = customerInfo.LastName,
                    PhoneNumber = customerInfo.PhoneNumber,
                    PostCode = customerInfo.PostCode
                }
            };
        }

        public class Product
        {
            public int ProductId { get; set; }
            public string Name { set; get; }
            public int Value { get; set; }
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public class Response
        {
            public ICollection<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int TotalCharge => Products.Sum(prod => prod.Value * prod.Quantity); 
        }

        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
