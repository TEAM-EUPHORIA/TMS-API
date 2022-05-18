using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Content { get; set; }
        public bool? isDisabled { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        // virtual navigation properties
        public virtual Course? Course { get; set; }
        public virtual List<Attendance>? Attendances { get; set; }
        public virtual List<Assignment>? Assignments { get; set; }
    }
}