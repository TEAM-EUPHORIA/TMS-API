using System.ComponentModel.DataAnnotations;

namespace TMS.BAL
{
    public class Department:AuditFields
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool? isDisabled { get; set; }
    }
}