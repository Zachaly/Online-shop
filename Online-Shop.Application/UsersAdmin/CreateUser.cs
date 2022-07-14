using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.UsersAdmin
{
    /// <summary>
    /// Creates manager user
    /// </summary>
    public class CreateManager
    {
        private readonly IUserManager _userManager;

        public CreateManager(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> ExecuteAsync(Request request)
        {
            var id = await _userManager.CreateManager(request.UserName, "zaq1@WSX");

            return new Response
            {
                Id = id,
                Username = request.UserName,
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
