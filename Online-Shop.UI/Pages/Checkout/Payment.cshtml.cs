using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Online_Shop.Application.Cart;
using Stripe;

namespace Online_Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; private set; }

        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
        }

        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var information = getCustomerInformation.Execute();

            if(information is null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }
    }
}
