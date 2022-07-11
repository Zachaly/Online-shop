using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_shop.DataBase;
using Online_Shop.Application.StockAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        [HttpGet("")]
        public IActionResult GetStocks([FromServices] GetStock getStock) => Ok(getStock.Execute());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request,
            [FromServices] CreateStock createStock)
            => Ok(await createStock.ExecuteAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id, [FromServices] DeleteStock deleteStock) 
            => Ok(await deleteStock.ExecuteAsync(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request,
            [FromServices] UpdateStock updateStock)
            => Ok(await updateStock.ExecuteAsync(request));
    }
}
