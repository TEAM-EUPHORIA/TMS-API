using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class CourseUsers:AuditFields
    {
        [Key,Column(Order = 0)]
        public int CourseId { get; set; }
        [Key,Column(Order = 1)]
        public int UserId { get; set; }
        [Key,Column(Order = 2)]
        public int RoleId { get; set; }
        // virtual navigation properties
        public virtual User? User { get; set; }
        public virtual Course? Course { get; set; }
    }
}