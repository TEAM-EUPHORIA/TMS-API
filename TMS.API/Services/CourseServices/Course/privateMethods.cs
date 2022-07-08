using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private List<CourseUsers> GetListOfValidUsers(AddUsersToCourse data, int currentUserId)
        {
            var validList = new List<CourseUsers>();
            bool courseUsertExists;
            foreach (var user in data.users!)
            {
                courseUsertExists = _repo.Validation.CourseUserExists(data.CourseId, user.UserId, user.RoleId);
                if (!courseUsertExists)
                {
                    var courseUser = new CourseUsers();
                    courseUser.CourseId = data.CourseId;
                    courseUser.UserId = user.UserId;
                    courseUser.RoleId = user.RoleId;
                    courseUser.CreatedOn = DateTime.Now;
                    courseUser.CreatedBy = currentUserId;
                    validList.Add(courseUser);
                }
            }
            return validList.Distinct().ToList();
        }
        private List<CourseUsers> GetCourseUsers(AddUsersToCourse data, int currentUserId)
        {
            var validList = new List<CourseUsers>();
            bool courseUsertExists;
            foreach (var user in data.users!)
            {
                courseUsertExists = _repo.Validation.CourseUserExists(data.CourseId, user.UserId, user.RoleId);
                if (courseUsertExists)
                {
                    var courseUser = new CourseUsers
                    {
                        CourseId = data.CourseId,
                        UserId = user.UserId,
                        RoleId = user.RoleId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = currentUserId
                    };
                    validList.Add(courseUser);
                }
            }
            return validList.Distinct().ToList();
        }
        private void SetUpCourseDetails(Course course)
        {
            course.isDisabled = false;
            var user = _repo.Users.GetUserById(course.TrainerId);
            var courseTrainer = new CourseUsers(){
                CourseId=course.Id,
                UserId=course.TrainerId,
                RoleId=3,
                CreatedOn = DateTime.Now,
                CreatedBy = course.CreatedBy
            };
            course.UserMapping = new List<CourseUsers>
            {
                courseTrainer
            };
            course.CreatedOn = DateTime.Now;
        }
        private static void SetUpCourseDetails(Course course,Course dbCourse)
        {
            dbCourse.DepartmentId = course.DepartmentId;
            dbCourse.Name = course.Name;
            dbCourse.Duration = course.Duration;
            dbCourse.Description = course.Description;
            dbCourse.isDisabled = course.isDisabled;
            dbCourse.UpdatedOn = DateTime.Now;
        }
        private static void Disable(int currentUserId,Course dbCourse)
        {
            dbCourse.isDisabled = true;
            dbCourse.UpdatedBy = currentUserId;
            dbCourse.UpdatedOn = DateTime.Now;
        } 
    }
}