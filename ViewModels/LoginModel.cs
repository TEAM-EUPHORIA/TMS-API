using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TMS.API.ViewModels
{
    public class LoginModel
    {
        [DisplayName("Name")]
        [Required]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}