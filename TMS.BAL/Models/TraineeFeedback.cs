namespace TMS.BAL
{
    public class TraineeFeedback
    {
        public int Id { get; set; }
        public int TraineeId { get; set; }
        public int TrainerId { get; set; }
        public int CourseId { get; set; }
        public string Feedback { get; set; }
        public bool? isDisabled { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? Trainee { get; set; }
        public virtual User? Trainer { get; set; }
    }
}