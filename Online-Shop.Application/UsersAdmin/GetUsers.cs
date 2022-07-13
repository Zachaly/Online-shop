using Online_shop.Database;

namespace Online_Shop.Application.UsersAdmin
{
    /// <summary>
    /// Gets all users except admin
    /// </summary>
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
