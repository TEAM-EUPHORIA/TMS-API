using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TMS.API.ViewModels
{
    public class LoginModel
    {
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
    }
}