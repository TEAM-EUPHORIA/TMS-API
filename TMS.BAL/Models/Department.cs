using System.ComponentModel.DataAnnotations;

namespace TMS.BAL
{
    public class Department : AuditFields
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? Name { get; set; }
        public bool? isDisabled { get; set; }
    }
}