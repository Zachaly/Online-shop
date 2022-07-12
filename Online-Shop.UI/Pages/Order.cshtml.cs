using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Shop.Application.Orders;

namespace Online_Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }

        public void OnGet(string reference, [FromServices] GetOrder getOrder)
        {
            Order = getOrder.Execute(reference);
        }
    }
}
