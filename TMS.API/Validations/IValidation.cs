using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public interface IValidation
    {
        bool AssignmentExists(int courseId, int topicId, int ownerId);
        bool AttendanceExists(int courseId, int topicId, int ownerId);
        bool CourseExists(int courseId);
        bool CourseFeedbackExists(int courseId, int traineeId);
        bool CourseUserExists(int courseId, int userId, int roleId);
        bool DepartmentExists(int departmentId);
        bool IsCourseNameAvailable(int courseId, int departmentId, string name);
        bool IsTopicNameAvailabe(int topicId, int courseId, string name);
        bool MOMExists(int reviewId, int traineeId);
        bool MOMReviewExists(int reviewId, int traineeId);
        bool ReviewerAvailabilityExists(int id, int reviewerId, DateTime reviewDate, DateTime reviewTime, int statusId);
        bool ReviewExists(int reviewId);
        bool ReviewStatusExists(int statusId);
        bool RoleExists(int roleId);
        bool TopicExists(int topicId, int courseId);
        bool TopicExists(int topicId);
        bool TopicValidationExists(int courseId, string name);
        bool TraineeAvailabilityExists(int id, int traineeId, DateTime reviewDate, DateTime reviewTime, int statusId);
        bool TraineeFeedbackExists(int courseId, int traineeId, int trainerId);
        bool UserExists(int userId);
        bool UserExists(int userId, int roleId);
        bool UserExists(LoginModel user);
        Dictionary<string, string> ValidateAssignment(Assignment assignment);
        Dictionary<string, string> ValidateAttendance(Attendance attendance);
        Dictionary<string, string> ValidateCourse(Course course);
        Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback);
        Dictionary<string, string> ValidateCourseUser(CourseUsers user);
        Dictionary<string, string> ValidateDepartment(Department departemnt);
        Dictionary<string, string> ValidateLoginDetails(LoginModel user);
        Dictionary<string, string> ValidateMOM(MOM mom);
        Dictionary<string, string> ValidateReview(Review review);
        Dictionary<string, string> ValidateTopic(Topic topic);
        Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback);
        Dictionary<string, string> ValidateUser(User user);
        public bool ValidateCourseAccess(int courseId, int userId);
    }

}