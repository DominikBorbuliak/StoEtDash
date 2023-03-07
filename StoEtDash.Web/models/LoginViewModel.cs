using System.ComponentModel.DataAnnotations;

namespace StoEtDash.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter the username.")]
        [DataType(DataType.Text)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter the password.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
