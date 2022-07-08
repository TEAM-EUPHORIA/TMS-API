using Microsoft.EntityFrameworkCore;
using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if(userExists)
            {
                return _repo.Courses.GetCoursesByUserId(userId); 
            }
            else throw new ArgumentException(nameof(userId));
        }
        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if(departmentExists)
            {
                return _repo.Courses.GetCoursesByDepartmentId(departmentId);
            }
            else throw new ArgumentException(nameof(departmentId));
        }
        public IEnumerable<Course> GetCourses()
        {
            return _repo.Courses.GetCourses();   
        }
        public Course GetCourseById(int courseId,int userId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            var userExists = _repo.Validation.UserExists(userId);
            if(courseExists)
            {
                return _repo.Courses.GetCourseById(courseId,userId);
            }
            throw new ArgumentException(nameof(courseId));
        }
        public Dictionary<string,string> CreateCourse(Course course)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var validation = _repo.Validation.ValidateCourse(course);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpCourseDetails(course);
                _repo.Courses.CreateCourse(course);
                _repo.Complete();
            }
            return validation;
        }
        public Dictionary<string,string> UpdateCourse(Course course)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var validation = _repo.Validation.ValidateCourse(course);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbCourse = _repo.Courses.GetCourseById(course.Id);
                SetUpCourseDetails(course, dbCourse);
                _repo.Courses.UpdateCourse(dbCourse);
                _repo.Complete();    
            }
            return validation;
        }
        public bool DisableCourse(int courseId, int currentUserId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if(courseExists)
            {
                var dbCourse = _repo.Courses.GetCourseById(courseId);
                disable(currentUserId,dbCourse);
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
        public Dictionary<string,List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data, int currentUserId)
        {
            var courseExists = _repo.Validation.CourseExists(data.CourseId);
            if(courseExists)
            {
                var result =  new Dictionary<string,List<CourseUsers>>();
                var validList = GetListOfValidUsers(data,currentUserId);
                _repo.Courses.AddUsersToCourse(validList);
                result.Add("the following records are created", validList);
                _repo.Complete();
                return result;
            }
            else throw new ArgumentException(nameof(data));
        }

        public Dictionary<string,List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data, int currentUserId)
        {
            var courseExists = _repo.Validation.CourseExists(data.CourseId);
            if(courseExists)
            {
                var result =  new Dictionary<string,List<CourseUsers>>();
                var validList = GetCourseUsers(data,currentUserId);
                _repo.Courses.RemoveUsersFromCourse(validList);
                result.Add("the following records are removed",validList); 
                _repo.Complete();
                return result;
            }
            else throw new ArgumentException(nameof(data));
        }
    }
}