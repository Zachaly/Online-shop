using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_shop.DataBase;
using Online_Shop.Application.ProductsAdmin;
using Online_Shop.Application.StockAdmin;
using Online_Shop.Application.UsersAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private CreateUser _createUser;
        private GetUsers _getUsers;
        private DeleteUser _deleteUser;

        public UsersController(CreateUser createUser, GetUsers getUsers, DeleteUser deleteUser)
        {
            _createUser = createUser;
            _getUsers = getUsers;
            _deleteUser = deleteUser;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Request request) 
            => Ok(await _createUser.Execute(request));
        

        [HttpGet("")]
        public IActionResult GetUsers() => Ok(_getUsers.Execute());


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id) => Ok(await _deleteUser.Execute(id));
    }
}
