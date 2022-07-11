using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
using Online_Shop.Application.Cart;
using Online_Shop.Application.Products;
using System.Threading.Tasks;

namespace Online_Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        public GetProduct.ProductViewModel Product { get; set; }
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public async Task<IActionResult> OnGet(string name, [FromServices] GetProduct getProduct)
        {
            Product = await getProduct.ExecuteAsync(name);

            if (Product is null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var stockAdded = await addToCart.ExecuteAsync(CartViewModel);

            if(stockAdded)
                return RedirectToPage("Cart");

            return Page();
        }
    }
}
