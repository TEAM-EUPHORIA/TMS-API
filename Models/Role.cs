using System;

namespace TMS.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
        public bool isDisabled { get; set; }
=======

        public bool isDisabled { get; set; }

>>>>>>> 52946caf757255edd0cdbd4a50bd9915160746cb
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
