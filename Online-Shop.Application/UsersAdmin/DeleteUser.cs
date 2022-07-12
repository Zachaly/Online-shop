using Microsoft.AspNetCore.Identity;

namespace Online_Shop.Application.UsersAdmin
{
    /// <summary>
    /// Removes given user
    /// </summary>
    public class DeleteUser
    {
        private UserManager<IdentityUser> _userManager;

        public DeleteUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExecuteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}
