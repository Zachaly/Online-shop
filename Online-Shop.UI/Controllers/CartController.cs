using Microsoft.AspNetCore.Mvc;
using Online_Shop.Domain.Infrastructure;
using Online_Shop.Application.Cart;
using System.Linq;
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

        [HttpPost("{stockId}/{quantity}")]
        public async Task<IActionResult> RemoveProduct(int stockId, int quantity, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = quantity
            };

            if(await removeFromCart.ExecuteAsync(request))
            {
                return Ok("Item removed from cart");
            }

            return BadRequest("Failed to remove from cart");
        }

        [HttpGet("")]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Execute().Sum(product => product.RealValue * product.Quantity);

            return View("Components/Cart/Small", totalValue.GetPriceString());
        }

        [HttpGet("")]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Execute();

            return PartialView("_CartPartial", cart);
        }
    }
}
