using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class Course : AuditFields
    {
        public int Id { get; set; }
        [Required]
        [NotMapped]
        public int TrainerId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? Duration { get; set; }
        [Required]
        [MinLength(20)]
        [MaxLength(1500)]
        public string? Description { get; set; }
        public bool? isDisabled { get; set; }
        // virtual navigation properties
        [NotMapped]
        public virtual User? Trainer { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Topic>? Topics { get; set; }
        public virtual List<CourseUsers>? UserMapping { get; set; }
        public virtual List<CourseFeedback>? Feedbacks { get; set; }
        public virtual List<TraineeFeedback>? TraineeFeedbacks { get; set; }
    }
}