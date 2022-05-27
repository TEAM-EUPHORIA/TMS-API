using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class MOM:AuditFields
    {
        [Key,Column(Order = 0)]
        public int ReviewId { get; set; }
        [Key,Column(Order = 1)]
        public int TraineeId { get; set; }
        [Required]
        public string Agenda { get; set; }
        [Required]
        public string MeetingNotes { get; set; }
        [Required]
        public string PurposeOfMeeting { get; set; }
        //virtual navigation properties
        public virtual Review? Review { get; set; }
        public virtual User? Trainee { get; set; }
    }
}