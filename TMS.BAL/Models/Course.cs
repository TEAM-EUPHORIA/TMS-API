using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Course
    {
        public Course()
        {
            User Trainer = new User();
            CourseStatus status= new CourseStatus();
            Department Department = new Department();
            List<Topic> Topics = new List<Topic>();
            List<User> Users = new List<User>();
            List<CourseFeedback> Feedbacks = new List<CourseFeedback>();
        }
        public int Id { get; set; }
        public int? StatusId { get; set; }
        [Required]
        [NotMapped]
        public int TrainerId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Description { get; set; }
        public bool? isDisabled { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        // virtual navigation properties
        [NotMapped]
        public virtual User? Trainer { get; set; }
        public virtual CourseStatus? Status { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Topic>? Topics { get; set; }
        public virtual List<User>? Users { get; set; }
        public virtual List<CourseFeedback>? Feedbacks { get; set; }
    }
}