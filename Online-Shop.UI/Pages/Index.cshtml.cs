using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Online_shop.DataBase;
using Online_Shop.Application.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private AppDbContext _dbContext;
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public IndexModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            Products = new GetProducts(_dbContext).Execute();
        }
    }
}
