using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Logout([FromServices] SignInManager<IdentityUser> manager)
        {
            await manager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
