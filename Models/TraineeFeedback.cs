using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.API.Models
{
    public class TraineeFeedback
    {
        public int Id { get; set; }
        public int TraineeId { get; set; }
        public int TrainerId { get; set; }
        public int CourseId { get; set; }
        public string Feedback { get; set; }

        public Course? Course { get; set; }
        public User? Trainee { get; set; }
        public User? Trainer { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
