namespace TMS.BAL
{
    public class MOM
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        public string Agenda { get; set; }
        public string MeetingNotes { get; set; }
        public string PurposeOfMeeting { get; set; }
        public bool? isDisabled { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public virtual Review Review { get; set; }
        public virtual MOMStatus? Status { get; set; }
        public virtual User? Owner { get; set; }
    }
}