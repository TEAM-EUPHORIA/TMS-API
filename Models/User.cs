using System;

namespace TMS.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[]? Image { get; set; }
        public string? EmployeeId { get; set; }
        public bool? isDisabled { get; set; }

        public Role? Role { get; set; }
        public Department? Department { get; set; }
        public List<Course>? Courses { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
