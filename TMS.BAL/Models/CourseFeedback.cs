namespace TMS.BAL
{
    public class CourseFeedback
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int OwnerId { get; set; }
        public string Feedback { get; set; }
        public float Rating { get; set; }
        public bool? isDisabled { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public virtual Course? Course { get; set; }
        public virtual User? Owner { get; set; }
    }
}