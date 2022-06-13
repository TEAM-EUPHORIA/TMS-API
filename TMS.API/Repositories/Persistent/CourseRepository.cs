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
            dbContext.RemoveRange(data);
        }

        public void CreateAssignment(Assignment assignment)
        {
            assignment.CreatedOn = DateTime.Now;
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

        public void DisableCourse(int courseId)
        {
            var data = dbContext.Courses.Find(courseId);
            if(data != null)
            {
                data.isDisabled = true;
                dbContext.Courses.Update(data);
            }
        }

        public void DisableTopic(int courseId,int topicId)
        {
            var data = dbContext.Topics.Where(t=>t.CourseId == courseId && t.TopicId == topicId).FirstOrDefault();
            if(data != null)
            {
                data.isDisabled = true;
                dbContext.Topics.Update(data);
            }
        }

        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            return dbContext.Assignments.Where(a=>
                    a.CourseId == courseId && 
                    a.TopicId == topicId && 
                    a.OwnerId == ownerId
                ).Include(a=>a.Owner).FirstOrDefault();
        }

        public Course GetCourseById(int courseId)
        {
            return dbContext.Courses.FirstOrDefault(c=>c.Id == courseId);
        }
        public Topic GetTopicById(int courseId,int topicId)
        {
            return dbContext.Topics.Where(t=>t.CourseId == courseId && t.TopicId == topicId).FirstOrDefault();
        }

        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId)
        {
            return dbContext.Assignments.Where(a=>a.TopicId == topicId).Include(a=>a.Owner);
        }

        public IEnumerable<Course> GetCourses()
        {
            return dbContext.Courses.ToList();
        }

        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            return dbContext.Courses.Where(c=>c.DepartmentId == departmentId);
        }

        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
           return dbContext.CourseUsers.Where(cu=>cu.UserId == userId).Select(cu=>cu.Course);
        }

        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            return dbContext.Topics.Where(t=>t.CourseId == courseId);   
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
    }
}