using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Contracts
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void CreateUser(User user);
    }
}
