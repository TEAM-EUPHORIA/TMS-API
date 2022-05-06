using System;

namespace TMS.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
=======
        public bool isDisabled { get; set; }
>>>>>>> 1fe932234a10319c0f2fae96b4ac19f00dca9bee
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
