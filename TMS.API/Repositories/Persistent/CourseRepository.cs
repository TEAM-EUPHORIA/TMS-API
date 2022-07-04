using Microsoft.EntityFrameworkCore;
using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Repositories
{
    class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext dbContext;

        public CourseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddUsersToCourse(List<CourseUsers> data)
        {
            dbContext.CourseUsers.AddRange(data);
        }

        public void RemoveUsersFromCourse(List<CourseUsers> data)
        {
            dbContext.CourseUsers.RemoveRange(data);
        }

        public void CreateAssignment(Assignment assignment)
        {
            dbContext.Assignments.Add(assignment);
        }

        public void CreateCourse(Course course)
        {
            dbContext.Courses.Add(course);
        }

        public void CreateTopic(Topic topic)
        {
            dbContext.Topics.Add(topic);
        }

        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            return dbContext.Assignments.Where(a=>
                    a.CourseId == courseId && 
                    a.TopicId == topicId && 
                    a.OwnerId == ownerId
                ).Include(a=>a.Owner).FirstOrDefault();
        }
        public Attendance GetAttendanceByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            return dbContext.Attendances.Where(a=>
                    a.CourseId == courseId && 
                    a.TopicId == topicId && 
                    a.OwnerId == ownerId
                ).Include(a=>a.Owner).FirstOrDefault();
        }

        public Course GetCourseById(int courseId)
        {
            var result = dbContext.Courses
                            .Where(c=>c.Id == courseId && c.isDisabled == false)
                            .Include(c=>c.Topics)
                            .FirstOrDefault();

            result.Trainer = dbContext.CourseUsers
                                .Where(cu=>cu.CourseId == result.Id && cu.RoleId == 3)
                                .Include(u=>u.User)
                                .Select(cu=>cu.User)
                                .FirstOrDefault();
            result.TrainerId = result.Trainer.Id;
            return result;
        }
        public Topic GetTopicById(int courseId,int topicId,int userId)
        {
            var result = dbContext.Topics
                            .Where(t=>t.CourseId == courseId && t.TopicId == topicId).Include(a => a.Attendances)
                            .FirstOrDefault();

            var trainerId = dbContext.CourseUsers
                                .Where(cu=>cu.CourseId == courseId && cu.RoleId == 3)
                                .FirstOrDefault()
                                .UserId;

            var assignment = GetAssignmentByCourseIdTopicIdAndOwnerId(courseId,topicId,trainerId);

            var present = dbContext.Assignments.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == userId);

            result.Assignments = new List<Assignment>(){assignment};
            if(present)
            {
                var attendance = GetAttendanceByCourseIdTopicIdAndOwnerId(courseId,topicId,userId);
                result.Attendances = new List<Attendance>();
                result.Attendances.Add(attendance);
            }
            return result;
        }
        public Topic GetTopicById(int courseId,int topicId)
        {
            var result = dbContext.Topics
                            .Where(t=>t.CourseId == courseId && t.TopicId == topicId && t.isDisabled == false)
                            .FirstOrDefault();
            return result;
        }

        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId)
        {
            return dbContext.Assignments
                    .Where(a=>a.TopicId == topicId)
                    .Include(a=>a.Owner);
        }

        public IEnumerable<Course> GetCourses()
        {
            return dbContext.Courses.Where(c => c.isDisabled == false).ToList();
        }

        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            return dbContext.Courses
                    .Where(c=>c.DepartmentId == departmentId && c.isDisabled == false);
        }

        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
           return dbContext.CourseUsers
                    .Where(cu=>cu.UserId == userId )
                    .Select(cu=>cu.Course);
        }

        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            return dbContext.Topics
                    .Where(t=>t.CourseId == courseId && t.isDisabled == false);   
        }

        public void UpdateAssignment(Assignment assignment)
        {
            dbContext.Assignments.Update(assignment);
        }

        public void UpdateCourse(Course course)
        {
            dbContext.Courses.Update(course);
        }

        public void UpdateTopic(Topic topic)
        {
            dbContext.Topics.Update(topic);
        }

        public void MarkAttendance(Attendance attendance)
        {
            dbContext.Attendances.Add(attendance);
        }

        public object GetCourseUsers(int courseId)
        {
            var data = dbContext.CourseUsers.Where(cu => cu.CourseId == courseId).Include(cu => cu.User).Select(data => new {
                courseId = data.CourseId,
                department = data.Course.Department.Name,
                courseName = data.Course.Name,
                id = data.UserId,
                fullName = data.User.FullName,
                roleId = 4
            });
            
            return data;
        }
    }
}