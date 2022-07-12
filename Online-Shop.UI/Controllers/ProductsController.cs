using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Application.ProductsAdmin;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        [HttpGet("")]
        public IActionResult GetProducts([FromServices] GetAdminProducts getProducts) => Ok(getProducts.Execute());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id, [FromServices] GetAdminProduct getProduct) 
            => Ok(getProduct.Execute(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request,
            [FromServices] CreateProduct createProduct) 
            => Ok(await createProduct.ExecuteAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromServices] DeleteProduct deleteProduct) 
            => Ok(await deleteProduct.ExecuteAsync(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request,
            [FromServices] UpdateProduct updateProduct) 
            => Ok(await updateProduct.ExecuteAsync(request));
    }
}
