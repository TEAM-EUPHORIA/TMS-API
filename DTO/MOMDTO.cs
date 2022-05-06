using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API.DTO
{
    public class MOMDTO
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        public string Agenda { get; set; }
        public string MeetingNotes { get; set; }
        public string PurposeOfMeeting { get; set; }

    }
}
