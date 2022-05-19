using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class Assignment
    {
        public int Id { get; set; }
        [Required]
        public int TopicId { get; set; }
        public int StatusId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Base64 { get; set; }
        public byte[]? Document { get; set; }

        //Audit Fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        // virtual navigation properties
        public virtual AssignmentStatus? Status { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? Owner { get; set; }
    }
}