using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class Topic:AuditFields
    {
        [Key]
        public int TopicId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Content { get; set; }
        public bool? isDisabled { get; set; }
        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual List<Attendance>? Attendances { get; set; }
        public virtual List<Assignment>? Assignments { get; set; }
    }
}