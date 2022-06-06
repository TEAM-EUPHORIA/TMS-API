using TMS.BAL;

namespace TMS.API.Services
{
    public partial class CourseService
    {
        private static void prepareAssignment(Assignment assignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            assignment.Base64 = PDF.header;
            assignment.Document = PDF.bytes;
        }
        private static void prepareAssignment(Assignment assignment, Assignment dbAssignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            dbAssignment.Base64 = PDF.header;
            dbAssignment.Document = PDF.bytes;
        }
    }
}

