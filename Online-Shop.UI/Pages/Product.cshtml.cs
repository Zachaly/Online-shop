using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
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
        public Test ProductTest { get; set; }

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
            var currentId = HttpContext.Session.GetString("id");

            HttpContext.Session.SetString("id", ProductTest.Id);

            return RedirectToPage("Index");
        }

        public class Test
        {
            public string Id { get; set; }
        }
    }
}
