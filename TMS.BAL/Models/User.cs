using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class User
    {
        public User()
        {
            Role Role = new Role();
            Department Department = new Department();
            List<Course> Courses = new List<Course>();
        }
        //model attribute
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        // [RegularExpression(
        //     @"^(?!*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
        //     ErrorMessage = "Enter a valid Name. Name must not contain any special character or numbers"
        //  )]
        public string Name { get; set; }
        [Required]
        // [RegularExpression(
        //     @"^[A-Za-z]\\w{5, 29}$",
        //     ErrorMessage = "Enter a valid UserName"
        //  )]
        public string UserName { get; set; }
        [Required]
        // [RegularExpression(
        //     @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&-+=()])(?=\\S+$).{8, 20}$",
        //     ErrorMessage = "Enter a Correct Password"
        //  )]
        public string Password { get; set; }
        [Required]
        // [RegularExpression(
        //     @"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$",
        //     ErrorMessage = "Enter a valid Email address"
        // )]
        public string Email { get; set; }
        [Required]
        public string base64Header { get; set; }
        // [Required]
        public byte[]? Image { get; set; }
        [Required]
        public string? EmployeeId { get; set; }
        [Required]
        public bool? isDisabled { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        //Foreign key relation
        public virtual Role? Role { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Course>? Courses { get; set; }
    }
}
