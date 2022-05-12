namespace TMS.BAL
{
    public class CourseFeedback
    {
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        [RegularExpression(
            @"([A-Za-z]{20,1000})*$",
            ErrorMessage = "Enter a Valid Feedback"
         )]
        public string Feedback { get; set; }
        [Required]
        public float Rating { get; set; }
        

        //Audit fields
        [JsonIgnore]
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        public int? UpdatedBy { get; set; }

        //Foreign key relation
        public virtual Course? Course { get; set; }
        public virtual User? Owner { get; set; }
    }
}