using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoEtDash.Web.Database.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "VARCHAR (60)")]
        public string Username { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR (128)")]
        public string Password { get; set; } = string.Empty;
    }
}
