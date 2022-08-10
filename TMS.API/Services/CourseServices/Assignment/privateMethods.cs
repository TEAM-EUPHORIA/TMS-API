using TMS.BAL;

namespace TMS.API.Services
{
    public partial class CourseService
    {
        /// <summary>
        /// used to split the meta date and base64 string. 
        /// The base64 strings is converted to byte[]. 
        /// used for setting up Document in Assignment model
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="createdBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void PrepareAssignment(Assignment assignment, int createdBy)
        {
            if (assignment is null)
            {
                throw new ArgumentException(nameof(assignment));
            }

            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64!);
            assignment.Base64 = PDF.Header!;
            assignment.Document = PDF.Bytes;
            assignment.CreatedOn = DateTime.Now;
            assignment.CreatedBy = createdBy;
        }

        /// <summary>
        /// used to split the meta date and base64 string. 
        /// The base64 strings is converted to byte[]. 
        /// used for setting up Document in Assignment model
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="dbAssignment"></param>
        /// <param name="updatedBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void PrepareAssignment(Assignment assignment, Assignment dbAssignment, int updatedBy)
        {
            if (assignment is null)
            {
                throw new ArgumentException(nameof(assignment));
            }

            if (dbAssignment is null)
            {
                throw new ArgumentException(nameof(dbAssignment));
            }

            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64!);
            dbAssignment.Base64 = PDF.Header!;
            dbAssignment.Document = PDF.Bytes;
            dbAssignment.UpdatedOn = DateTime.Now;
            dbAssignment.UpdatedBy = updatedBy;
        }
    }
}

