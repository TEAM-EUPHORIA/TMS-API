using Microsoft.EntityFrameworkCore;
using TMS.API.ViewModels;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        public IEnumerable<Course> GetCoursesByUserId(int userId,AppDbContext dbContext)
        {
            var userExists = Validation.UserExists(dbContext,userId);
            if(userExists)
            {
                var mappingData = dbContext.CourseUsers.Where(cu=>cu.UserId==userId).Include(cu=>cu.Course).ToList();
                var result = new List<Course>();
                foreach (var item in mappingData)
                {
                    if(item.Course is not null) result.Add(item.Course); 
                }
                return result;   
            }
            else throw new ArgumentException(nameof(userId));
        }
        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId,AppDbContext dbContext)
        {
            var departmentExists = Validation.DepartmentExists(dbContext,departmentId);
            if(departmentExists)
            {
                return dbContext.Courses.Where(c => c.DepartmentId == departmentId);
            }
            else throw new ArgumentException(nameof(departmentId));
        }
        public IEnumerable<Course> GetCourses(AppDbContext dbContext)
        {
            return dbContext.Courses.ToList();   
        }
        public Course GetCourseById(int courseId,AppDbContext dbContext)
        {
            var courseExists = Validation.CourseExists(dbContext,courseId);
            if(courseExists)
            {
                var result = dbContext.Courses.Find(courseId);
                if(result is not null) return result;
            }
            throw new ArgumentException(nameof(courseId));
        }
        public Dictionary<string,string> CreateCourse(Course course,AppDbContext dbContext)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var validation = Validation.ValidateCourse(course,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpCourseDetails(course,dbContext);
                CreateAndSaveCourse(course,dbContext);
            }
            return validation;
        }
        public Dictionary<string,string> UpdateCourse(Course course,AppDbContext dbContext)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var validation = Validation.ValidateCourse(course,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbCourse = dbContext.Courses.Find(course.Id);
                if(dbCourse is not null)
                {
                    SetUpCourseDetails(course, dbCourse);
                    UpdateAndSaveCourse(dbCourse,dbContext);      
                }
            }
            return validation;
        }
        public bool DisableCourse(int courseId,AppDbContext dbContext)
        {
            var courseExists = Validation.CourseExists(dbContext,courseId);
            if(courseExists)
            {
                var dbCourse = dbContext.Courses.Find(courseId);
                var ab=dbContext.Courses.Where(u=>u.Id==courseId).SingleOrDefault();
                if(dbCourse is not null)
                {
                    dbCourse.isDisabled = !ab.isDisabled;
                    dbCourse.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveCourse(dbCourse,dbContext);
                }  
            }
            return courseExists;        
        }
        public Dictionary<string,List<CourseUsers>> AddUsersToCourse(AddUsersToCourse data,AppDbContext dbContext)
        {
            var courseExists = Validation.CourseExists(dbContext,data.CourseId);
            var result =  new Dictionary<string,List<CourseUsers>>();
            if(courseExists)
            {
                var validList = new List<CourseUsers>();
                bool courseUsertExists = false;
                foreach (var user in data.users)
                {
                    courseUsertExists = Validation.CourseUserExists(dbContext,data.CourseId,user.UserId,user.RoleId);
                    if(!courseUsertExists)
                    {
                        var courseUser = new CourseUsers();
                        courseUser.CourseId = data.CourseId;
                        courseUser.UserId = user.UserId;
                        courseUser.RoleId = user.RoleId;
                        validList.Add(courseUser);
                    }
                }  
                validList =validList.Distinct().ToList();
                AddUsersToCourseAndSave(validList,dbContext); 
                result.Add("the following records are created",validList); 
                return result;
            }
            else throw new ArgumentException(nameof(data));
        }
        public Dictionary<string,List<CourseUsers>> RemoveUsersFromCourse(AddUsersToCourse data,AppDbContext dbContext)
        {
            var validList = new List<CourseUsers>();
            bool courseUsertExists = false;
            foreach (var user in data.users)
            {
                courseUsertExists = Validation.CourseUserExists(dbContext,data.CourseId,user.UserId,user.RoleId);
                if(courseUsertExists)
                {
                    var courseUser = new CourseUsers();
                    courseUser.CourseId = data.CourseId;
                    courseUser.UserId = user.UserId;
                    courseUser.RoleId = user.RoleId;
                    validList.Add(courseUser);
                }
            }  
            validList =validList.Distinct().ToList();
            RemoveUsersFromCourseAndSave(validList,dbContext); 
            var result =  new Dictionary<string,List<CourseUsers>>();
            result.Add("the following records are removed",validList); 
            return result;
        }
    }
}