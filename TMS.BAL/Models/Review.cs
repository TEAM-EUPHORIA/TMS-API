using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Review
    {
        public Review()
        {
             User Reviewer = new User();
             User Trainee = new User();
             MOM MOM = new MOM();
             ReviewStatus Status = new ReviewStatus();
        }
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        public int ReviewerId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        [RegularExpression(
            @"^(1[0-2]|0[1-9])/(3[01] + |[12][0-9]|0[1-9])/[0-9]{4}$",
            ErrorMessage = "Enter a valid Date."
        )]
        public string ReviewDate { get; set; }
        [Required]
        [RegularExpression(
            @"^(1[0-2]|0[1-9])/(3[01] + |[12][0-9]|0[1-9])/[0-9]{4}$",
            ErrorMessage = "Enter a valid Date."
        )]
        public string ReviewTime { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z]{6,10})*$",
            ErrorMessage = "Enter a Valid Content"
         )]
        public string Mode { get; set; }
        [Required]
        public bool? isDisabled { get; set; }

        //Foreign key relation
        public User? Reviewer { get; set; }
        public User? Trainee { get; set; }
        [NotMapped]
        public MOM? MOM { get; set; }
        public ReviewStatus? Status { get; set; }

        //Foreign key relation
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}