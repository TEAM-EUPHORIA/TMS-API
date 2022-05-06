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
<<<<<<< HEAD

=======
<<<<<<< HEAD
        public bool? isDisabled { get; set; }

=======
        public bool isDisabled { get; set; }
>>>>>>> 1fe932234a10319c0f2fae96b4ac19f00dca9bee
>>>>>>> 07fe34b4a7b220e1bd9c3a79440ae1f2984bd355
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