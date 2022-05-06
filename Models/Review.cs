using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int StatusId { get; set; }
        public int TraineeId { get; set; }
        public string ReviewDate { get; set; }
        public string ReviewTime { get; set; }
        public string Mode { get; set; }
        public bool isDisabled { get; set; }
        public User? Reviewer { get; set; }
        public User? Trainee { get; set; }
        [NotMapped]
        public MOM? MOM { get; set; }
        public ReviewStatus? Status { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}