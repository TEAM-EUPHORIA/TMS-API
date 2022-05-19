using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class MOM
    {
        public int Id { get; set; }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Agenda { get; set; }
        [Required]
        public string MeetingNotes { get; set; }
        [Required]
        public string PurposeOfMeeting { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        //virtual navigation properties
        public virtual Review Review { get; set; }
        public virtual MOMStatus? Status { get; set; }
        public virtual User? Owner { get; set; }
    }
}