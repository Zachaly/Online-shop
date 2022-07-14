using Microsoft.AspNetCore.Identity;
using Online_Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Database
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _dbContext;

        public UserManager(UserManager<IdentityUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<string> CreateManager(string username, string password)
        {
            var manager = new IdentityUser
            {
                UserName = username,
            };

            await _userManager.CreateAsync(manager, password);

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(manager, managerClaim);

            return manager.Id;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public IEnumerable<object> GetUsers()
            => _dbContext.Users.Where(user => user.UserName != "Admin");
    }
}
