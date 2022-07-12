using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Shop.Application.Products;
using System.Collections.Generic;

namespace Online_Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet([FromServices] GetProducts getProducts)
        {
            Products = getProducts.Execute();
        }
    }
}
