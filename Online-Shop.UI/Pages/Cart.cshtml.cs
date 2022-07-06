using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;
using System.Collections.Generic;

namespace Online_Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        private AppDbContext _dbContext;

        public IEnumerable<GetCart.Response> Cart { get; set; }

        public CartModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _dbContext).Execute();

            return Page();
        }
    }
}
