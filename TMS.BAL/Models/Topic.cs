namespace TMS.BAL
{
    public class Topic
    {
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        [RegularExpression(
            @"^(?!*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Topic Name.Topic It must not contain any special character or numbers"
         )]
        public string Name { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Context { get; set; }
        [Required]
        public bool? isDisabled { get; set; }

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
        public virtual List<Attendance>? Attendances { get; set; }
        public virtual List<Assigment>? Assigments { get; set; }

    }
}