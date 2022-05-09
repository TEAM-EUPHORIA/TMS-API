using System;
using System.Collections.Generic;

namespace TMS.API.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Context { get; set; }
        public bool isDisabled { get; set; }
        public Course? Course { get; set; }
        public List<Attendance>? Attendances { get; set; }
        public List<Assigment>? Assigments { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

    }
}