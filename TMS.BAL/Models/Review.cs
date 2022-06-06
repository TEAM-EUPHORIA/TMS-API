using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class Review:AuditFields
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public int ReviewerId { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public DateTime ReviewTime { get; set; }
        [Required]
        public string Mode { get; set; }
        //virtual navigation properties
        public virtual User? Reviewer { get; set; }
        public virtual User? Trainee { get; set; }
        public virtual ReviewStatus? Status { get; set; }
        public virtual Department? Department { get; set; }
    }
}