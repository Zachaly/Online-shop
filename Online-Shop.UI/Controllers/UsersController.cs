using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Application.UsersAdmin;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Request request,
            [FromServices] CreateUser createUser) 
            => Ok(await createUser.ExecuteAsync(request));
        
        [HttpGet("")]
        public IActionResult GetUsers([FromServices] GetUsers getUsers) => Ok(getUsers.Execute());

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id, [FromServices] DeleteUser deleteUser) 
            => Ok(await deleteUser.ExecuteAsync(id));
    }
}
