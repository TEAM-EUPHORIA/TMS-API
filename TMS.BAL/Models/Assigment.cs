using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class Assigment
    {
        //model attributes
        [Required]
        public int Id { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [NotMapped]
        public string base64 { get; set; }
        [Required]
        public byte[]? Document { get; set; }

        //Audit Fields
        [JsonIgnore]
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        public int? UpdatedBy { get; set; }
        
        //Foreign key relation
        public virtual AssignmentStatus? Status { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? Owner { get; set; }
    }
}