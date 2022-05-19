using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class TraineeFeedback
    {
        public TraineeFeedback()
        {
            Course? Course = new Course();
            User? Trainee = new User();
            User? Trainer = new User();
        }
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int TrainerId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$",
            ErrorMessage = "Enter a Valid Feedback"
         )]
        public string Feedback { get; set; }
        // [Required]
        public bool? isDisabled { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        //Foreign key relation
        public virtual Course? Course { get; set; }
        public virtual User? Trainee { get; set; }
        public virtual User? Trainer { get; set; }
    }
}