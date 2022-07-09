using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_shop.DataBase;
using Online_Shop.Application.Orders;

namespace Online_Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        private AppDbContext _dbContext;

        public GetOrder.Response Order { get; set; }

        public OrderModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(string reference)
        {
            Order = new GetOrder(_dbContext).Execute(reference);
        }
    }
}
