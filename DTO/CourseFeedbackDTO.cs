using System;

namespace TMS.API.DTO
{
    public class CourseFeedbackDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int OwnerId { get; set; }
        public string Feddback { get; set; }
        public float Rating { get; set; }

    }
}