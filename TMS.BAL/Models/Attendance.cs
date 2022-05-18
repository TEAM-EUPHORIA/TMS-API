using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Attendance
    {
        public int Id { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int OwnerId { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        // virtual navigation properties
        public virtual AttendanceStatus? Status { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? Owner { get; set; }
    }
}