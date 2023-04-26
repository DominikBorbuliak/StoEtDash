using StoEtDash.Web.Database.Models;

namespace StoEtDash.Web.Database.Contracts
{
	public interface IUserRepository
	{
		/// <summary>
		/// Finds user by username
		/// Throws UserException with appropiate text when user not found
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		User GetUserByUsername(string username);

		/// <summary>
		/// Adds user to database
		/// Throws UserException with appropiate text when user already exists
		/// </summary>
		/// <param name="user"></param>
		void CreateUser(User user);
	}
}
