namespace TMS.DAL
{
    public class Topic
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Context { get; set; }
        public bool? isDisabled { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual Course? Course { get; set; }
        public virtual List<Attendance>? Attendances { get; set; }
        public virtual List<Assigment>? Assigments { get; set; }

    }
}