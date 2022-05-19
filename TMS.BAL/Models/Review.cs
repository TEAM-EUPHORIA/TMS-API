using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int ReviewerId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public string ReviewDate { get; set; }
        [Required]
        public string ReviewTime { get; set; }
        [Required]
        public string Mode { get; set; }
        public bool? isDisabled { get; set; }

        //virtual navigation properties
        public virtual User? Reviewer { get; set; }
        public virtual User? Trainee { get; set; }
        public virtual ReviewStatus? Status { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}