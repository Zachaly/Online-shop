using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Application.OrdersAdmin;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        [HttpGet("")]
        public IActionResult GetOrders(int status, [FromServices] GetOrders getOrders) 
            => Ok(getOrders.Execute(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id, [FromServices] GetOrder getOrder) 
            => Ok(getOrder.Execute(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromServices] UpdateOrder updateOrder) 
            => Ok(await updateOrder.ExecuteAsync(id));
    }
}
