using System.ComponentModel.DataAnnotations;

namespace StoEtDash.Web.Models
{
	public class RegisterViewModel : LoginViewModel
	{
		[Required(ErrorMessage = "Please enter the password again.")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Passwords must match!")]
		public string? ConfirmPassword { get; set; }
	}
}
