using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class CourseFeedback:AuditFields
    {
        [Key,Column(Order = 0)]
        public int CourseId { get; set; }
        [Key,Column(Order = 1)]
        public int TraineeId { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(1000)] 
        public string Feedback { get; set; }
        [Required]
        [Range(1.0, 5)]
        public float Rating { get; set; }
        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual User? Trainee { get; set; }
    }
}