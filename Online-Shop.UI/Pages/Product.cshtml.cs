using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;
using Online_Shop.Application.Products;

namespace Online_Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        private AppDbContext _dbContext;

        public ProductModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetProduct.ProductViewModel Product { get; set; }
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }


        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_dbContext).Execute(name);

            if (Product is null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Execute(CartViewModel);

            return RedirectToPage("Cart");
        }

    }
}
