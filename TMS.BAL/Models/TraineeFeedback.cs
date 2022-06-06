using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class TraineeFeedback:AuditFields
    {
        [Key,Column(Order = 0)]
        public int CourseId { get; set; }
        [Key,Column(Order = 1)]
        public int TraineeId { get; set; }
        [Key,Column(Order = 2)]
        public int TrainerId { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)] 
        public string Feedback { get; set; }
        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual User? Trainee { get; set; }
        public virtual User? Trainer { get; set; }
    }
}