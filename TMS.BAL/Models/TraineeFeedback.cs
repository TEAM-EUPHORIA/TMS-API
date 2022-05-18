using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class TraineeFeedback
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int TrainerId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Feedback { get; set; }
        public bool? isDisabled { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual User? Trainee { get; set; }
        public virtual User? Trainer { get; set; }
        public virtual TraineeFeedbackStatus? Status { get; set; }
    }
}