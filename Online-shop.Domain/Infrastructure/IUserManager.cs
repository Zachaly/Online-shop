
namespace Online_Shop.Domain.Infrastructure
{
    public interface IUserManager
    {
        Task<string> CreateManager(string username, string password);
        Task<bool> DeleteUser(string id);
        IEnumerable<object> GetUsers();
    }
}
