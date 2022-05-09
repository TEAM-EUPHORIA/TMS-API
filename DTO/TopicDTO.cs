using System;
using System.Collections.Generic;
namespace TMS.API.DTO
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Context { get; set; }

    }
}