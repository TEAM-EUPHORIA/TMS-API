namespace TMS.BAL
{
    public class AuditFields
    {
        //Audit Fields
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}