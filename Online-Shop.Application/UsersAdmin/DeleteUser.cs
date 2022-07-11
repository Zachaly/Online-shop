using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.UsersAdmin
{
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
