using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Topic
    {
        public Topic()
        {
            Course Course = new Course();
            List<Attendance> Attendances = new List<Attendance>();
            List<Assignment> Assignments = new List<Assignment>();
        }
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        [RegularExpression(
            @"^(?!*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Topic Name.Topic It must not contain any special character or numbers"
         )]
        public string Name { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Context { get; set; }
        [Required]
        public bool? isDisabled { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        //Foreign key relation
        public virtual Course? Course { get; set; }
        public virtual List<Attendance>? Attendances { get; set; }
        public virtual List<Assignment>? Assignments { get; set; }

    }
}