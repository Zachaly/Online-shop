using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentController : Controller
    {
        private AppDbContext _dbContext;

        public PaymentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var cartOrder = new GetOrder(HttpContext.Session, _dbContext).Execute();

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = cartOrder.TotalCharge,
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }
        public class Item
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }
    }
}

