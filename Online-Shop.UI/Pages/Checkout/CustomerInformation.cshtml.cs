using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;

namespace Online_Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Execute();

            if(information is null)
            {
                return Page();
            }

            return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost()
        {
            new AddCustomerInformation(HttpContext.Session).Execute(CustomerInformation);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
