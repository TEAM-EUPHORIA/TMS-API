using System;

namespace TMS.API.DTO
{
    public class AssigmentDTO
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        public byte[] Document { get; set; }

    }
}