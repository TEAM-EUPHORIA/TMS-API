using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Services
{
    public interface ICourseService
    {
        Dictionary<string, List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data, int updatedBy);
        Dictionary<string, string> CreateAssignment(Assignment assignment, int createdBy);
        Dictionary<string, string> CreateCourse(Course course, int createdBy);
        Dictionary<string, string> CreateTopic(Topic topic, int createdBy);
        Dictionary<string, string> UpdateTopic(Topic topic);
        bool DisableCourse(int courseId, int updatedBy);
        bool DisableTopic(int courseId, int topicId, int updatedBy);
        Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId);
        IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId);
        Course GetCourseById(int courseId,int userId);
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCoursesByDepartmentId(int departmentId);
        IEnumerable<Course> GetCoursesByUserId(int userId);
        object GetCourseUsers(int courseId);
        Topic GetTopicById(int courseId, int topicId, int userId);
        IEnumerable<Topic> GetTopicsByCourseId(int courseId);
        Dictionary<string, string> MarkAttendance(Attendance attendance);
        IEnumerable<Attendance> GetAttendanceList(int courseId,int topicId);
        Dictionary<string, List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data, int updatedBy);
        Dictionary<string, string> UpdateAssignment(Assignment assignment, int updatedBy);
        Dictionary<string, string> UpdateCourse(Course course, int updatedBy);
        Dictionary<string, string> UpdateTopic(Topic topic, int updatedBy);
    }
}