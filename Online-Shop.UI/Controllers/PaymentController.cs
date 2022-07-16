using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shop.Application.Cart;
using Online_Shop.Application.Orders;
using Stripe;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    /// <summary>
    /// Example from stripe website
    /// </summary>
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Create(PaymentIntentCreateRequest request,
            [FromServices] Application.Cart.GetOrder getCartOrder,
            [FromServices] CreateOrder createOrder,
            [FromServices] ClearCart clearCart)
        {
            var cartOrder = getCartOrder.Execute();

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

            var sessionId = HttpContext.Session.Id;

            await createOrder.ExecuteAsync(new CreateOrder.Request
            {
                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Address = cartOrder.CustomerInformation.Address,
                City = cartOrder.CustomerInformation.City,
                PostCode = cartOrder.CustomerInformation.PostCode,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                StripeId = paymentIntent.Id,
                SessionId = sessionId,
                Stocks = cartOrder.Products.Select(prod => new CreateOrder.Stock
                {
                    StockId = prod.StockId,
                    Quantity = prod.Quantity,
                }).ToList(),
            });

            clearCart.Execute();

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

