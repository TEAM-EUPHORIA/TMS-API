using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class CourseService
    {
        /// <summary>
        /// used to get course based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// IEnumerable course if course is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists)
            {
                return _repo.Courses.GetCoursesByUserId(userId);
            }
            else throw new ArgumentException(INVALID_ID);
        }
        /// <summary>
        /// used to get course based on department id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>
        /// IEnumerable course if department is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                return _repo.Courses.GetCoursesByDepartmentId(departmentId);
            }
            else throw new ArgumentException(INVALID_ID);
        }
        /// <summary>
        /// used to get course
        /// </summary>
        /// <returns>
        /// IEnumerable course 
        /// </returns>
        public IEnumerable<Course> GetCourses()
        {
            return _repo.Courses.GetCourses();
        }
        /// <summary>
        /// used to get course by user id,courseId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// course if course and user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Course GetCourseById(int courseId, int userId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            var userExists = _repo.Validation.UserExists(userId);
            if (courseExists && userExists)
            {
                return _repo.Courses.GetCourseById(courseId, userId);
            }
            else throw new ArgumentException(INVALID_ID);
        }
        /// <summary>
        /// used to create a course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> CreateCourse(Course course, int createdBy)
        {
            if (course is null) throw new ArgumentException(nameof(course));
            var validation = _repo.Validation.ValidateCourse(course);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpCourseDetails(course, createdBy);
                _repo.Courses.CreateCourse(course);
                _repo.Complete();
            }
            return validation;
        }
        /// <summary>
        /// used to update a course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> UpdateCourse(Course course, int updatedBy)
        {
            if (course is null) throw new ArgumentException(nameof(course));
            var validation = _repo.Validation.ValidateCourse(course);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbCourse = _repo.Courses.GetCourseById(course.Id);
                SetUpCourseDetails(course, dbCourse, updatedBy);
                _repo.Courses.UpdateCourse(dbCourse);
                _repo.Complete();
            }
            return validation;
        }
        /// <summary>
        /// used to disable course to user.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// true if course is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public bool DisableCourse(int courseId, int updatedBy)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if (courseExists)
            {
                var dbCourse = _repo.Courses.GetCourseById(courseId);
                Disable(updatedBy, dbCourse);
                _repo.Complete();
            }
            return courseExists;
        }
        /// <summary>
        /// used to get course user from course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>
        /// true if course users is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public object GetCourseUsers(int courseId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if (courseExists)
            {
                return _repo.Courses.GetCourseUsers(courseId);
            }
            else throw new ArgumentException(INVALID_ID);
        }
        /// <summary>
        /// used to add list of users to course.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data, int createdBy)
        {
            if (data is null)
            {
                throw new ArgumentException(nameof(data));
            }
            var courseExists = _repo.Validation.CourseExists(data.CourseId);
            if (courseExists)
            {
                var result = new Dictionary<string, List<CourseUsers>>();
                var validList = GetListOfValidUsers(data, createdBy);
                _repo.Courses.AddUsersToCourse(validList);
                result.Add("the following records are created", validList);
                _repo.Complete();
                return result;
            }
            else throw new ArgumentException("Invalid course id");
        }
        /// <summary>
        /// used remove users from course.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data, int updatedBy)
        {
            if (data is null)
            {
                throw new ArgumentException(nameof(data));
            }
            var courseExists = _repo.Validation.CourseExists(data.CourseId);
            if (courseExists)
            {
                var result = new Dictionary<string, List<CourseUsers>>();
                var validList = GetCourseUsers(data, updatedBy);
                _repo.Courses.RemoveUsersFromCourse(validList);
                result.Add("the following records are removed", validList);
                _repo.Complete();
                return result;
            }
            else throw new ArgumentException("Invalid course id");
        }
    }
}