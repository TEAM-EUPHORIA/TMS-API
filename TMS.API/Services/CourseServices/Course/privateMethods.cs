using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class CourseService
    {
        /// <summary>
        /// used to get list of valid users.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// returns list of valid user
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private List<CourseUsers> GetListOfValidUsers(AddUsersToCourse data, int createdBy)
        {
            if (data is null)
            {
                throw new ArgumentException(nameof(data));
            }

            var validList = new List<CourseUsers>();
            bool courseUsertExists;
            foreach (var user in data.Users!)
            {
                courseUsertExists = _repo.Validation.CourseUserExists(data.CourseId, user.UserId, user.RoleId);
                if (!courseUsertExists)
                {
                    var courseUser = new CourseUsers
                    {
                        CourseId = data.CourseId,
                        UserId = user.UserId,
                        RoleId = user.RoleId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = createdBy
                    };
                    validList.Add(courseUser);
                }
            }
            return validList.Distinct().ToList();
        }
        /// <summary>
        /// used to get course users.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// returns list of Course user
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private List<CourseUsers> GetCourseUsers(AddUsersToCourse data, int createdBy)
        {
            if (data is null)
            {
                throw new ArgumentException(nameof(data));
            }

            var validList = new List<CourseUsers>();
            bool courseUsertExists;
            foreach (var user in data.Users!)
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
                        CreatedBy = createdBy
                    };
                    validList.Add(courseUser);
                }
            }
            return validList.Distinct().ToList();
        }
        /// <summary>
        /// used to set up Course details with course and createdBy.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="createdBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpCourseDetails(Course course, int createdBy)
        {
            if (course is null)
            {
                throw new ArgumentException(nameof(course));
            }

            course.isDisabled = false;
            var courseTrainer = new CourseUsers()
            {
                CourseId = course.Id,
                UserId = course.TrainerId,
                RoleId = 3,
                CreatedOn = DateTime.Now,
                CreatedBy = course.CreatedBy
            };
            course.UserMapping = new List<CourseUsers>
            {
                courseTrainer
            };
            course.CreatedOn = DateTime.Now;
            course.CreatedBy = createdBy;
        }
        /// <summary>
        /// used to set up Course details with course,dbcourse and updatedBy.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="dbCourse"></param>
        /// <param name="updatedBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpCourseDetails(Course course, Course dbCourse, int updatedBy)
        {
            if (course is null)
            {
                throw new ArgumentException(nameof(course));
            }

            if (dbCourse is null)
            {
                throw new ArgumentException(nameof(dbCourse));
            }

            dbCourse.DepartmentId = course.DepartmentId;
            dbCourse.Name = course.Name;
            dbCourse.Duration = course.Duration;
            dbCourse.Description = course.Description;
            dbCourse.isDisabled = course.isDisabled;
            dbCourse.UpdatedOn = DateTime.Now;
            dbCourse.UpdatedBy = updatedBy;
        }
        /// <summary>
        /// used to disable the course.
        /// </summary>
        /// <param name="dbCourse"></param>
        /// <param name="updatedBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void Disable(int updatedBy, Course dbCourse)
        {
            if (dbCourse is null)
            {
                throw new ArgumentException(nameof(dbCourse));
            }

            dbCourse.isDisabled = true;
            dbCourse.UpdatedBy = updatedBy;
            dbCourse.UpdatedOn = DateTime.Now;
        }
    }
}