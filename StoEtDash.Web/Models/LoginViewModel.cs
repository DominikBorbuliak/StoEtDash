using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Extensions;
using System.ComponentModel.DataAnnotations;

namespace StoEtDash.Web.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Please enter the username.")]
		[DataType(DataType.Text)]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter the password.")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
	}

	public static class LoginViewModelMapper
	{
		public static User ToDatabaseModel(this LoginViewModel loginViewModel) => new()
		{
			Username = loginViewModel.Username,
			Password = loginViewModel.Password.ToSha512()
		};
	}
}
