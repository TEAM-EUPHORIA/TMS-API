using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class CourseFeedback
    {
        public CourseFeedback()
        {
            Course Course = new Course();
            User Owner = new User();
        }
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z]{20,1000})*$",
            ErrorMessage = "Enter a Valid Feedback"
         )]
        public string Feedback { get; set; }
        [Required]
        public float Rating { get; set; }
        
        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        //Foreign key relation
        public virtual Course? Course { get; set; }
        public virtual User? Owner { get; set; }
    }
}