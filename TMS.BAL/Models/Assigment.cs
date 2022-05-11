using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.BAL
{
    public class Assigment
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        [NotMapped]
        public string base64 { get; set; }
        public byte[]? Document { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public virtual AssignmentStatus? Status { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual User? Owner { get; set; }
    }
}