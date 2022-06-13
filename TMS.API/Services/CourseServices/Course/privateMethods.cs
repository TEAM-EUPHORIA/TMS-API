using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private List<CourseUsers> GetListOfValidUsers(AddUsersToCourse data)
        {
            var validList = new List<CourseUsers>();
            bool courseUsertExists = false;
            foreach (var user in data.users)
            {
                courseUsertExists = _repo.Validation.CourseUserExists(data.CourseId, user.UserId, user.RoleId);
                if (!courseUsertExists)
                {
                    var courseUser = new CourseUsers();
                    courseUser.CourseId = data.CourseId;
                    courseUser.UserId = user.UserId;
                    courseUser.RoleId = user.RoleId;
                    validList.Add(courseUser);
                }
            }
            return validList.Distinct().ToList();
        }
        private void SetUpCourseDetails(Course course)
        {
            var user = _repo.Users.GetUserById(course.TrainerId);
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
            dbCourse.UpdatedOn = DateTime.UtcNow;
        }
    }
}