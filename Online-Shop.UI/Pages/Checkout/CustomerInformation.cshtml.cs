using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Online_Shop.Application.Cart;

namespace Online_Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation,
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var information = getCustomerInformation.Execute();

            if(information is null)
            {
                // fill form with default data while testing
                if (webHostEnvironment.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "jaroslaw",
                        LastName = "kaczynski",
                        Address = "wiejska",
                        City = "warszawa",
                        PostCode = "01-641",
                        Email = "j.kaczynski@sejm.gov",
                        PhoneNumber = "666666666",
                    };
                }

                return Page();
            }

            return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            addCustomerInformation.Execute(CustomerInformation);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
