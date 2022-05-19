using System.ComponentModel.DataAnnotations;

namespace TMS.BAL
{
    public class CourseStatus
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}