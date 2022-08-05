using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class MOM : AuditFields
    {
        [Key, Column(Order = 0)]
        public int ReviewId { get; set; }
        [Key, Column(Order = 1)]
        public int TraineeId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Agenda { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string? MeetingNotes { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? PurposeOfMeeting { get; set; }
        //virtual navigation properties
        public virtual Review? Review { get; set; }
        public virtual User? Trainee { get; set; }
    }
}