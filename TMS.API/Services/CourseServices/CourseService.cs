using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseService> _logger;

        public CourseService(AppDbContext context, ILogger<CourseService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Course> GetCoursesByUserId(int userId)
        {
           if (userId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetCoursesByUserId));
            try
            {
                var result = _context.Users.Where(u=>u.Id==userId).Include(u=>u.Courses).FirstOrDefault();
                return result.Courses;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetCoursesByUserId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCoursesByUserId));
                throw;
            }
        }
        public IEnumerable<Course> GetCoursesByDepartmentId(int departmentId)
        {
            if (departmentId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetCoursesByDepartmentId));
            try
            {
                return _context.Courses.Where(c => c.DepartmentId == departmentId);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetCoursesByDepartmentId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCoursesByDepartmentId));
                throw;
            }
        }
        public IEnumerable<Course> GetCourses()
        {
            try
            {
                return _context.Courses.ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetCourses));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCourses));
                throw;
            }            
        }
        public Course GetCourseById(int courseId)
        {
            if (courseId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetCourseById));
            try
            {
                return _context.Courses.Find(courseId);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetCourseById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCourseById));
                throw;
            }
        }
        public Dictionary<string,string> CreateCourse(Course course)
        {
             if (course == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateCourse), nameof(course));
            var validation = Validation.ValidateCourse(course,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    SetUpCourseDetails(course);
                    CreateAndSaveCourse(course);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(CreateCourse));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(CreateCourse));
                    throw;
                }
            }
            return validation;
        }
        public Dictionary<string,string> UpdateCourse(Course course)
        {
            if (course == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateCourse), nameof(course));
            var validation = Validation.ValidateCourse(course,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbCourse = _context.Courses.Where(c=>c.Id==course.Id).Include(c=>c.Users).FirstOrDefault();
                    if (dbCourse != null)
                    {
                        SetUpCourseDetails(course, dbCourse);
                        UpdateAndSaveCourse(dbCourse);
                    }
                    validation.Add("Invalid Id","Not Found");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(UpdateCourse));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(UpdateCourse));
                    throw;
                }
            }
            return validation;
        }
        public bool DisableCourse(int courseId)
        {
           if (courseId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableCourse));
            try
            {
                var dbCourse = _context.Courses.Find(courseId);
                if (dbCourse != null)
                {
                    dbCourse.isDisabled = true;
                    dbCourse.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveCourse(dbCourse);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(DisableCourse));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(DisableCourse));
                throw;
            }
        }
        private void UpdateAndSaveCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
        private void CreateAndSaveCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        private void SetUpCourseDetails(Course course)
        {
            var user = _context.Users.Find(course.TrainerId);
            course.Users = new List<User>();
            course.Users.Add(user);
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