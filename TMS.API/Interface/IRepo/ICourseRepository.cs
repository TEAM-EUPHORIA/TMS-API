using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Repositories
{
    public partial interface ICourseRepository
    {
        IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId);
        IEnumerable<Attendance> GetAttendanceList(int couresId,int topicId);

        Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId);
        void CreateAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCoursesByDepartmentId(int departmentId);
        IEnumerable<Course> GetCoursesByUserId(int userId);
        object GetCourseUsers(int courseId);
        Course GetCourseById(int courseId,int userId);
        Course GetCourseById(int courseId);
        void CreateCourse(Course course);
        void UpdateCourse(Course course);

        void AddUsersToCourse(List<CourseUsers> data);
        void RemoveUsersFromCourse(List<CourseUsers> data);
        void MarkAttendance(Attendance attendance);


        IEnumerable<Topic> GetTopicsByCourseId(int courseId);
        Topic GetTopicById(int courseId,int topicId,int userId);
        Topic GetTopicById(int courseId,int topicId);
        void CreateTopic(Topic topic);
        void UpdateTopic(Topic topic);
    }
}