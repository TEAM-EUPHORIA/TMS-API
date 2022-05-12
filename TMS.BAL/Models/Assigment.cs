using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Assigment
    {
        public Assigment()
        {
            AssignmentStatus Status = new AssignmentStatus();
            Topic Topic = new Topic();
            User Owner = new User();
        }
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
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        
        //Foreign key relation
        public virtual AssignmentStatus? Status { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? Owner { get; set; }
    }
}