using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> Execute(Request request)
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
