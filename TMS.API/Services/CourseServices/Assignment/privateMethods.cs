using TMS.BAL;

namespace TMS.API.Services
{
    public partial class CourseService
    {
        private static void PrepareAssignment(Assignment assignment, int createdBy)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            assignment.Base64 = PDF.Header!;
            assignment.Document = PDF.Bytes;
            assignment.CreatedOn = DateTime.Now;
            assignment.CreatedBy = createdBy;
        }
        private static void PrepareAssignment(Assignment assignment, Assignment dbAssignment, int updatedBy)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            dbAssignment.Base64 = PDF.Header!;
            dbAssignment.Document = PDF.Bytes;
            dbAssignment.UpdatedOn = DateTime.Now;
            dbAssignment.UpdatedBy = updatedBy;
        }
    }
}

