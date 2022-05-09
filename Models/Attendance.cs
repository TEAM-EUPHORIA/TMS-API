using System;

namespace TMS.API.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }

        public AttendanceStatus? Status { get; set; }
        public Topic? Topic { get; set; }
        public User? Owner { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}