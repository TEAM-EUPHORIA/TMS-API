using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TMS.API.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        [DisplayName("Name")]
        [Required]
        [RegularExpression(
            @"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Name. Name must not contain any special character or numbers"
         )]
        public string Name { get; set; }
        [DisplayName("User Name")]
        [Required]
        [RegularExpression(
            @"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z0-9]*$",
            ErrorMessage = "Enter a valid User Name. User Name must not contain any special character"
         )]
        public string UserName { get; set; }
        [DisplayName("Password")]
        [Required]
        [RegularExpression(
            @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Enter a valid Password. Password must contain Minimum eight characters, at least one letter, one number and one special character"
         )]
        public string Password { get; set; }
        [DisplayName("Email")]
        [Required]
        [RegularExpression(
            @"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$",
            ErrorMessage = "Enter a valid Email address"
        )]
        public string Email { get; set; }
        [Required]
        public IFormFile image { get; set; }
        public byte[]? Image { get; set; }

    }
}
