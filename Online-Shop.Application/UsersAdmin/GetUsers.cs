using Microsoft.AspNetCore.Identity;
using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.UsersAdmin
{
    /// <summary>
    /// Gets all users except admin
    /// </summary>
    public class GetUsers
    {
        private IUserManager _userManager;

        public GetUsers(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<UserViewModel> Execute()
            => _userManager.GetUsers().Select(user => user as IdentityUser).
                Select(user => new UserViewModel
                {
                    Username = user.UserName,
                    Id = user.Id,
                }).ToList();


        public class UserViewModel 
        {
            public string Username { get; set; }
            public string Id { get; set; }
        }
    }
}
