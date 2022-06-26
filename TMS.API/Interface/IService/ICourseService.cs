using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Services
{
    public interface ICourseService
    {
        Dictionary<string, List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data, int currentUserId);
        Dictionary<string, string> CreateAssignment(Assignment assignment);
        Dictionary<string, string> CreateCourse(Course course);
        Dictionary<string, string> CreateTopic(Topic topic);
        bool DisableCourse(int courseId, int currentUserId);
        bool DisableTopic(int courseId, int topicId, int currentUserId);
        Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId);
        IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId);
        Course GetCourseById(int courseId);
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCoursesByDepartmentId(int departmentId);
        IEnumerable<Course> GetCoursesByUserId(int userId);
        Topic GetTopicById(int courseId, int topicId, int userId);
        IEnumerable<Topic> GetTopicsByCourseId(int courseId);
        Dictionary<string, string> MarkAttendance(Attendance attendance);
        Dictionary<string, List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data, int currentUserId);
        Dictionary<string, string> UpdateAssignment(Assignment assignment);
        Dictionary<string, string> UpdateCourse(Course course);
        Dictionary<string, string> UpdateTopic(Topic topic);
    }
}