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
            if(userExists)
            {
                return _repo.Courses.GetCoursesByUserId(userId); 
            }
            else throw new ArgumentException(nameof(userId));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCoursesByUserId));
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
            if(departmentExists)
            {
                return _repo.Courses.GetCoursesByDepartmentId(departmentId);
            }
            else throw new ArgumentException(nameof(departmentId));
            }
            
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCoursesByDepartmentId));
                throw;
            }


        }

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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Course GetCourseById(int courseId,int userId)
        {
            try
            {
            var courseExists = _repo.Validation.CourseExists(courseId);
            var userExists = _repo.Validation.UserExists(userId);
            if(courseExists && userExists)
            {
                return _repo.Courses.GetCourseById(courseId,userId);
            }
            throw new ArgumentException(nameof(courseId));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetCourseById));
                throw;
            }
        }
        public Dictionary<string,string> CreateCourse(Course course, int createdBy)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var validation = _repo.Validation.ValidateCourse(course);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpCourseDetails(course,createdBy);
                _repo.Courses.CreateCourse(course);
                _repo.Complete();
            }
            return validation;
        }
        public Dictionary<string,string> UpdateCourse(Course course, int updatedBy)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var validation = _repo.Validation.ValidateCourse(course);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbCourse = _repo.Courses.GetCourseById(course.Id);
                SetUpCourseDetails(course, dbCourse,updatedBy);
                _repo.Courses.UpdateCourse(dbCourse);
                _repo.Complete();    
            }
            return validation;
        }
        public bool DisableCourse(int courseId, int updatedBy)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if(courseExists)
            {
                var dbCourse = _repo.Courses.GetCourseById(courseId);
                Disable(updatedBy,dbCourse);
                _repo.Complete();
            }
            return courseExists;        
        }
        public object GetCourseUsers(int courseId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if(courseExists)
            {
                return _repo.Courses.GetCourseUsers(courseId);
            }
            else throw new ArgumentException(nameof(courseId));
        }
        public Dictionary<string,List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data, int createdBy)
        {
            var courseExists = _repo.Validation.CourseExists(data.CourseId);
            if(courseExists)
            {
                var result =  new Dictionary<string,List<CourseUsers>>();
                var validList = GetListOfValidUsers(data,createdBy);
                _repo.Courses.AddUsersToCourse(validList);
                result.Add("the following records are created", validList);
                _repo.Complete();
                return result;
            }
            else throw new ArgumentException(nameof(data));
        }

        public Dictionary<string,List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data, int updatedBy)
        {
            var courseExists = _repo.Validation.CourseExists(data.CourseId);
            if(courseExists)
            {
                var result =  new Dictionary<string,List<CourseUsers>>();
                var validList = GetCourseUsers(data,updatedBy);
                _repo.Courses.RemoveUsersFromCourse(validList);
                result.Add("the following records are removed",validList); 
                _repo.Complete();
                return result;
            }
            else throw new ArgumentException(nameof(data));
        }
    }
}