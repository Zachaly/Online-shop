using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Shop.Application.Cart;

namespace Online_Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Execute();

            if(information is null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }
    }
}
