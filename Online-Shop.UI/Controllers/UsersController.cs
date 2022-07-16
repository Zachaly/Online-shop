using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.UI.ViewModels.Admin;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel viewModel)
        {
            var manager = new IdentityUser
            {
                UserName = viewModel.Username,
            };

            await _userManager.CreateAsync(manager, viewModel.Password);

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(manager, managerClaim);

            return Ok(new { username = manager.UserName, id = manager.Id });
        }

        [HttpGet("")]
        public IActionResult GetUsers() 
        {
            var users = _userManager.Users.Where(user => user.UserName != "Admin").Select(user => new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName
            }).ToList();

            return Ok(users); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);

            return Ok(result.Succeeded);
        }
            
    }
}
