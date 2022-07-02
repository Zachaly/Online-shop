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
        public IActionResult CreateProduct(CreateProduct.ProductViewModel viewModel) 
            => Ok(new CreateProduct(_dbContext).Execute(viewModel));

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id) => Ok(new DeleteProduct(_dbContext).Execute(id));

        [HttpPut("products")]
        public IActionResult UpdateProduct(UpdateProduct.ProductViewModel viewModel)
            => Ok(new UpdateProduct(_dbContext).Execute(viewModel));
    }
}
