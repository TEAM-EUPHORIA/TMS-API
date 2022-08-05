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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
            try
            {
                var userExists = _repo.Validation.UserExists(userId);
                if (userExists)
                {
                    return _repo.Courses.GetCoursesByUserId(userId);
                }
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCoursesByUserId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetCoursesByUserId));
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            try
            {

                var departmentExists = _repo.Validation.DepartmentExists(departmentId);
                if (departmentExists)
                {
                    return _repo.Courses.GetCoursesByDepartmentId(departmentId);
                }
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCoursesByDepartmentId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetCoursesByDepartmentId));
                throw;
            }

        }
        /// <summary>
        /// used to get course
        /// </summary>
        /// <returns>
        /// IEnumerable course 
        /// </returns>

        public IEnumerable<Course> GetCourses()
        {
            try
            {
                return _repo.Courses.GetCourses();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCourses));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetCourses));
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Course GetCourseById(int courseId, int userId)
        {
            try
            {
                var courseExists = _repo.Validation.CourseExists(courseId);
                var userExists = _repo.Validation.UserExists(userId);
                if (courseExists && userExists)
                {
                    return _repo.Courses.GetCourseById(courseId, userId);
                }
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCourseById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetCourseById));
                throw;
            }
        }

        /// <summary>
        /// used to create a course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> CreateCourse(Course course, int createdBy)
        {
            try
            {
                if (course is null) throw new ArgumentNullException(nameof(course));
                var validation = _repo.Validation.ValidateCourse(course);
                if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
                {
                    SetUpCourseDetails(course, createdBy);
                    _repo.Courses.CreateCourse(course);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(CreateCourse));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CreateCourse));
                throw;
            }
        }

        /// <summary>
        /// used to update a course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> UpdateCourse(Course course, int updatedBy)
        {
            try
            {
                if (course is null) throw new ArgumentNullException(nameof(course));
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
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(UpdateCourse));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UpdateCourse));
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public bool DisableCourse(int courseId, int updatedBy)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(DisableCourse));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DisableCourse));
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public object GetCourseUsers(int courseId)
        {
            try
            {
                var courseExists = _repo.Validation.CourseExists(courseId);
                if (courseExists)
                {
                    return _repo.Courses.GetCourseUsers(courseId);
                }
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCourseUsers));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetCourseUsers));
                throw;
            }
        }

        /// <summary>
        /// used to add list of users to course.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Dictionary<string, List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data, int createdBy)
        {
            try
            {
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
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(AddUsersToCourse));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(AddUsersToCourse));
                throw;
            }
        }
        /// <summary>
        /// used remove users from course.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data, int updatedBy)
        {
            try
            {
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
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(RemoveUsersFromCourse));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(RemoveUsersFromCourse));
                throw;
            }

        }
    }
}