using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class MOM
    {
        public MOM()
        {
            Review Review = new Review();
            MOMStatus Status = new MOMStatus();
            User Owner = new User();
        }
        //model attributes
        [Required]
        public int Id { get; set; }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z0-9]{20,250})*$",
            ErrorMessage = "Enter a Valid Content"
         )]
        public string Agenda { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z]{20,1000})*$",
            ErrorMessage = "Enter a Meeting Notes"
         )]
        public string MeetingNotes { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z]{20,1000})*$",
            ErrorMessage = "Enter a Purpose of meeting"
         )]
        public string PurposeOfMeeting { get; set; }
        
        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        //Foreign key relation
        public virtual Review Review { get; set; }
        public virtual MOMStatus? Status { get; set; }
        public virtual User? Owner { get; set; }
    }
}