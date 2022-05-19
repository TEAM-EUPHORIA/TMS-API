using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class CourseFeedback
    {
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Feedback { get; set; }
        [Required]
        public float Rating { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual User? Owner { get; set; }
    }
}