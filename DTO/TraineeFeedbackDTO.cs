using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace TMS.API.DTO
{
    public class TraineeFeedbackDTO
    {
        public int Id { get; set; }
        public int TraineeId { get; set; }
        public int CourseId { get; set; }
        public int TrainerId { get; set; }
        public string Feedback { get; set; }

    }
}
