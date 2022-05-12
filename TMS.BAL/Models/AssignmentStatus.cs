using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMS.BAL
{
    public class AssignmentStatus
    {
        //model attributes
        [Required]
        public int Id { get; set; }
        [RegularExpression(
            @"^(?!*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Name for Assignment status, It must not contain any special character or numbers"
         )]
        public string Name { get; set; }

        //Audit fields
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}