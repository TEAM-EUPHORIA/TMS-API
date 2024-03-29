using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class Attendance : AuditFields
    {
        [Key, Column(Order = 0)]
        public int CourseId { get; set; }
        [Key, Column(Order = 1)]
        public int TopicId { get; set; }
        [Key, Column(Order = 2)]
        public int OwnerId { get; set; }
        [Required]
        public bool Status { get; set; }
        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? Owner { get; set; }
    }
}