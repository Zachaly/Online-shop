using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Online_Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost([FromServices] SignInManager<IdentityUser> signInManager)
        {
            var result =  await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Admin/Index");
            }

            return Page();
        }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
