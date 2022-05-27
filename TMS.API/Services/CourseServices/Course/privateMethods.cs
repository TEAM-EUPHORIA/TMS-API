using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private void AddUsersToCourseAndSave(List<CourseUsers> validList, AppDbContext dbContext)
        {
            dbContext.CourseUsers.AddRange(validList);
            dbContext.SaveChanges();
        }
        private void RemoveUsersFromCourseAndSave(List<CourseUsers> validList, AppDbContext dbContext)
        {
            dbContext.CourseUsers.RemoveRange(validList);
            dbContext.SaveChanges();
        }
        private void UpdateAndSaveCourse(Course course,AppDbContext dbContext)
        {
            dbContext.Courses.Update(course);
            dbContext.SaveChanges();
        }
        private void CreateAndSaveCourse(Course course,AppDbContext dbContext)
        {
            dbContext.Courses.Add(course);
            dbContext.SaveChanges();
        }
        private void SetUpCourseDetails(Course course,AppDbContext dbContext)
        {
            var user = dbContext.Users.Find(course.TrainerId);
            var courseTrainer = new CourseUsers(){CourseId=course.Id,UserId=course.TrainerId,RoleId=3};
            course.UserMapping = new List<CourseUsers>();
            course.UserMapping.Add(courseTrainer);
            course.CreatedOn = DateTime.UtcNow;
        }
        private void SetUpCourseDetails(Course course,Course dbCourse)
        {
            dbCourse.DepartmentId = course.DepartmentId;
            dbCourse.Name = course.Name;
            dbCourse.Duration = course.Duration;
            dbCourse.Description = course.Description;
            course.UpdatedOn = DateTime.UtcNow;
        }
    }
}