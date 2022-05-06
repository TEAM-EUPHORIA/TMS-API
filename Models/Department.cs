using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 52946caf757255edd0cdbd4a50bd9915160746cb
        public bool isDisabled { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
