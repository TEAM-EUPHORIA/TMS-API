using System.ComponentModel.DataAnnotations;

namespace TMS.BAL
{
    public class ReviewStatus:AuditFields
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
