namespace StoEtDash.Web.Database.Models
{
	/// <summary>
	/// Exceptions that will be shown to user as notifications
	/// </summary>
	public class UserException : Exception
	{
		public UserException() { }
		public UserException(string message) : base(message) { }
	}
}
