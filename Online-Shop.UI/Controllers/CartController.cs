using Microsoft.AspNetCore.Mvc;
using Online_Shop.Application.Cart;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            if(await addToCart.ExecuteAsync(request))
            {
                return Ok("Item added to cart");
            }

            return BadRequest("Failed adding to cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            if(await removeFromCart.ExecuteAsync(request))
            {
                return Ok("Item removed from cart");
            }

            return BadRequest("Failed to remove from cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            if (await removeFromCart.ExecuteAsync(request))
            {
                return Ok("Removed all from cart");
            }

            return BadRequest("Failed to remove all from cart");
        }
    }
}
