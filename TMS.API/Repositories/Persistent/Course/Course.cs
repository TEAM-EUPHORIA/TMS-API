using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class CourseRepository : ICourseRepository
    {
        public void AddUsersToCourse(List<CourseUsers> data)
        {
            dbContext.CourseUsers.AddRange(data);
        }
        public void RemoveUsersFromCourse(List<CourseUsers> data)
        {
            dbContext.CourseUsers.RemoveRange(data);
        }
        public void CreateCourse(Course course)
        {
            dbContext.Courses.Add(course);
        }
        public Course GetCourseById(int courseId, int userId)
        {
            var result = dbContext.Courses
                            .Where(c => c.Id == courseId &&
                                        c.isDisabled == false)
                            .FirstOrDefault();
            result!.Topics = GetTopicsByCourseId(courseId).ToList();
            result!.Feedbacks = dbContext.CourseFeedbacks
                            .Where(cf => cf.CourseId == courseId)
                            .Include(cf => cf.Trainee).ToList();
            result!.Trainer = dbContext.CourseUsers
                                .Where(cu => cu.CourseId == result.Id &&
                                             cu.RoleId == 3)
                                .Include(u => u.User)
                                .Select(cu => cu.User)
                                .FirstOrDefault();
            result.TrainerId = result!.Trainer!.Id;
            return result;
        }
        public Course GetCourseById(int courseId)
        {
            var result = dbContext.Courses
                            .Where(c => c.Id == courseId &&
                                        c.isDisabled == false)
                            .FirstOrDefault();
            result!.Trainer = dbContext.CourseUsers
                                .Where(cu => cu.CourseId == result.Id &&
                                              cu.RoleId == 3)
                                .Include(u => u.User)
                                .Select(cu => cu.User)
                                .FirstOrDefault();
            result.TrainerId = result!.Trainer!.Id;
            return result;
        }
        public IEnumerable<Course> GetCourses()
        {
            return dbContext.Courses.Where(c => c.isDisabled == false).Include(c => c.Department).ToList();
        }
        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            return dbContext.Courses
                    .Where(c => c.DepartmentId == departmentId &&
                                c.isDisabled == false);
        }
        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
            return dbContext.CourseUsers
                     .Where(cu => cu.UserId == userId)
                     .Select(cu => cu.Course)!;
        }
        public void UpdateCourse(Course course)
        {
            dbContext.Courses.Update(course);
        }
        public IEnumerable<User> GetCourseUsers(int courseId)
        {
            var data = dbContext.CourseUsers
                            .Where(cu => cu.CourseId == courseId && cu.User!.isDisabled == false)
                            .Include(cu => cu.User).Select(cu => cu.User).ToList();
            return data!;
        }
    }
}