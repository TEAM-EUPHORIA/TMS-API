namespace TMS.API
{
    public partial class Validation
    {
        public bool ValidateCourseAccess(int courseId, int userId)
        {
            var result = dbContext.CourseUsers.Any(cu => cu.CourseId == courseId && cu.UserId == userId && cu.Course.isDisabled == false && cu.User.isDisabled == false);
            return result;
        }
    }
}