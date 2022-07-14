using Microsoft.AspNetCore.Identity;
using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.UsersAdmin
{
    /// <summary>
    /// Removes given user
    /// </summary>
    public class DeleteUser
    {
        private readonly IUserManager _userManager;

        public DeleteUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExecuteAsync(string id)
            => await _userManager.DeleteUser(id);
    }
}
