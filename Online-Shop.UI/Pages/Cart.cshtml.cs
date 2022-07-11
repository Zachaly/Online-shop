using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;
using System.Collections.Generic;

namespace Online_Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetCart.Response> Cart { get; set; }

        public IActionResult OnGet([FromServices] GetCart getCart)
        {
            Cart = getCart.Execute();

            return Page();
        }
    }
}
