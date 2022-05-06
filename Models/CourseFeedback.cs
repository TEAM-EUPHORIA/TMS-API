using System;

namespace TMS.API.Models
{
    public class CourseFeedback
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int OwnerId { get; set; }
        public string Feddback { get; set; }
        public float Rating { get; set; }

        public Course? Course { get; set; }
        public User? Owner { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}