namespace TMS.BAL
{
    public class TraineeFeedBackStatus
    {
        //model attribute
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(
            @"^(?!*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Feedback status name. It must not contain any special character or numbers"
        )]
        public string Name { get; set; }

        //Audit fields
        [JsonIgnore]
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        public int? UpdatedBy { get; set; }
    }
}
