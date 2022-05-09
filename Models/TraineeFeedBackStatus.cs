using System;

namespace TMS.API.Models
{
    public class TraineeFeedBackStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TraineeFeedBackStatus? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}