using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API.Models
{
    public class MOM
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        public string Agenda { get; set; }
        public string MeetingNotes { get; set; }
        public string PurposeOfMeeting { get; set; }

        public Review Review { get; set; }
        public MOMStatus? Status { get; set; }
        public User? Owner { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
