using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;

namespace Online_Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public CustomerInformationModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Execute();

            if(information is null)
            {
                if (_hostingEnvironment.IsDevelopment())
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
