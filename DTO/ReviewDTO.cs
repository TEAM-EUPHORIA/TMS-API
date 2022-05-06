using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.API.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int StatusId { get; set; }
        public int TraineeId { get; set; }
        public string ReviewDate { get; set; }
        public string ReviewTime { get; set; }
        public string Mode { get; set; }

    }
}