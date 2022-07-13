using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Online_Shop.Application.UsersAdmin
{
    /// <summary>
    /// Creates manager user
    /// </summary>
    public class CreateManager
    {
        private UserManager<IdentityUser> _userManager;

        public CreateManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var manager = new IdentityUser
            {
                UserName = request.UserName,
            };

            await _userManager.CreateAsync(manager);

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(manager, managerClaim);

            return new Response
            {
                Id = manager.Id,
                Username = manager.UserName,
            };
        }

        public class Request
        {
            public string UserName { get; set; }
        }

        public class Response
        {
            public string Id { get; set; }
            public string Username { get; set; }
        }
    }
}
