using System.ComponentModel.DataAnnotations.Schema;
namespace TMS.DAL
{
    public class Course
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        [NotMapped]
        public int TrainerId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }

        public bool? isDisabled { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        [NotMapped]
        public virtual User? Trainer { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Topic>? Topics { get; set; }
        public virtual List<User>? Users { get; set; }
        public virtual List<CourseFeedback>? Feedbacks { get; set; }
    }
}