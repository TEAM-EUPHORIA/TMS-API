using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class User : AuditFields
    {
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? FullName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Password { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Email { get; set; }
        [Required]
        public string? Base64 { get; set; }
        public byte[]? Image { get; set; }
        public string? EmployeeId { get; set; }
        public bool? isDisabled { get; set; }
        // virtual navigation properties
        public virtual Role? Role { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<CourseUsers>? CourseMapping { get; set; }
        [NotMapped]
        public virtual bool FeedBackExists { get; set; }
        [NotMapped]
        public virtual int CourseId { get; set; }
    }
}
