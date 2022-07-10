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
    [Route("Admin/[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        private AppDbContext _dbContext;

        public StocksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public IActionResult GetStocks() => Ok(new GetStock(_dbContext).Execute());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request)
            => Ok(await new CreateStock(_dbContext).Execute(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(_dbContext).Execute(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request)
            => Ok(await new UpdateStock(_dbContext).Execute(request));
    }
}
