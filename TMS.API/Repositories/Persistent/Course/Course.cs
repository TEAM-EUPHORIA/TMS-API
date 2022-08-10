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
            result!.TraineeFeedbacks = dbContext.TraineeFeedbacks.Where(c => c.CourseId == courseId).ToList();
            result.TrainerId = result!.Trainer!.Id;
            return result;
        }
        public IEnumerable<Course> GetCourses()
        {
            return dbContext.Courses.Include(c => c.Department).Where(c => c.isDisabled == false).OrderByDescending(c => c.CreatedOn).ToList()  ;
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
                     .Where(cu => cu.UserId == userId && cu.Course!.isDisabled == false)
                     .Select(cu => cu.Course)!;
        }
        public void UpdateCourse(Course course)
        {
            dbContext.Courses.Update(course);
        }
        public IEnumerable<User> GetCourseUsers(int courseId)
        {
            var data = dbContext.CourseUsers
                            .Where(cu => cu.CourseId == courseId && cu.User!.isDisabled == false && cu.User!.RoleId != 3)
                            .Include(cu => cu.User).ToList();
            var result = new List<User>();
            bool feedbackExist = false;
            foreach (var item in data)
            {
               feedbackExist = dbContext.TraineeFeedbacks.Any(tf => tf.CourseId == item.CourseId && tf.TraineeId == item.UserId);
                item.User!.FeedBackExists = feedbackExist;
                result.Add(item.User);
            }
            return result!;
        }
    }
}