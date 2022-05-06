﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API.Models
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

<<<<<<< HEAD
        public CourseStatus? Status { get; set; }
        [NotMapped]
        public virtual User? Trainer { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Topic>? Topics { get; set; }
        public virtual List<User>? Trainees { get; set; }
        public virtual List<CourseFeedback>? Feedbacks { get; set; }
=======
<<<<<<< HEAD
=======
        public bool isDisabled { get; set; }
>>>>>>> 1fe932234a10319c0f2fae96b4ac19f00dca9bee
        public CourseStatus? Status { get; set; }
        [NotMapped]
        public User? Trainer { get; set; }
        public Department? Department { get; set; }
        public List<Topic>? Topics { get; set; }
        public List<User>? Trainees { get; set; }
        public List<CourseFeedback>? Feedbacks { get; set; }
>>>>>>> 07fe34b4a7b220e1bd9c3a79440ae1f2984bd355

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
