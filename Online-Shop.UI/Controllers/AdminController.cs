using Microsoft.AspNetCore.Mvc;
using Online_shop.DataBase;
using Online_Shop.Application.ProductsAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private AppDbContext _dbContext;

        public AdminController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_dbContext).Execute());

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_dbContext).Execute(id));

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request) 
            => Ok(await new CreateProduct(_dbContext).Execute(request));

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_dbContext).Execute(id));

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request)
            => Ok(await new UpdateProduct(_dbContext).Execute(request));
    }
}
