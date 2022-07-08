using TMS.BAL;

namespace TMS.API.Services
{
    public partial class CourseService
    {
        private static void PrepareAssignment(Assignment assignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            assignment.Base64 = PDF.header!;
            assignment.Document = PDF.bytes;
            assignment.CreatedOn = DateTime.Now;
        }
        private static void PrepareAssignment(Assignment assignment, Assignment dbAssignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            dbAssignment.Base64 = PDF.header!;
            dbAssignment.Document = PDF.bytes;
            dbAssignment.UpdatedOn = DateTime.Now;
        }
    }
}

