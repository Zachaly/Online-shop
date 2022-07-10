using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.UsersAdmin
{
    public class GetUsers
    {
        private AppDbContext _dbContext;

        public GetUsers(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserViewModel> Execute()
            => _dbContext.Users.Where(user => user.UserName != "Admin").
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
