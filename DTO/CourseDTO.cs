using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        [NotMapped]
        public int TrainerId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }

    }
}
