using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IUserRepository _userRepository;

        public DatabaseService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByUsername(string username) => _userRepository.GetUserByUsername(username);

        public void CreateUser(LoginViewModel loginViewModel) => _userRepository.CreateUser(loginViewModel.ToDatabaseModel());
    }
}
