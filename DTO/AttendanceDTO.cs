using System;

namespace TMS.API.DTO
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
    }
}