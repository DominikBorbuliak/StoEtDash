using StoEtDash.Web.Database.Contracts;
using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Data
{
	public class UserRepository : IUserRepository
	{
		public User GetUserByUsername(string username)
		{
			using var dbContext = new StoEtDashContext();

			var user = dbContext.Users.FirstOrDefault(user => user.Username.Equals(username));

			if (user == null)
			{
				throw new UserException("User with provided username does not exists.");
			}

			return user;
		}

		public void CreateUser(User user)
		{
			using var dbContext = new StoEtDashContext();

			if (dbContext.Users.Any(userDb => userDb.Username.Equals(user.Username)))
			{
				throw new UserException("User with provided username already exists.");
			}

			dbContext.Users.Add(user);
			dbContext.SaveChanges();
		}
	}
}
