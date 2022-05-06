using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;

namespace TMS.API.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DashboardService> _logger;
        private readonly UserService _userService;
        private readonly CourseService _courseService;
        private readonly DepartmentService _departmentService;

        public DashboardService(AppDbContext context, ILogger<DashboardService> logger, UserService userService, CourseService courseService, DepartmentService departmentService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
            _courseService = courseService;
            _departmentService = departmentService;
        }
        public object getUserCount(){
            int coordinatorCount = _userService.GetUsersByRole(2).Count();
            int trainerCount = _userService.GetUsersByRole(2).Count();
            int traineeCount = _userService.GetUsersByRole(1).Count();
            int reviewerCount = _userService.GetUsersByRole(2).Count();
            return new {
                coordinatorCount,
                trainerCount,
                traineeCount,
                reviewerCount
            };
        }

        public object getCourseCount(){
             int courseCount = _courseService.GetAllCourses().Count();
            return new {
                courseCount
            };
        }

        // public object getDepartmentCount(){
        //     int departmentCount = _departmentService.GetAllDepartments().Count();
        //     return departmentCount;
        // }
        public IEnumerable<Course> D()
        {
            try
            {
                return _context.Courses.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

        public Object GetCourseById(int id)
        {
            var obj = _context.Courses.Find(id);
            if (obj != null)   
            {
                return obj;
            }
            return 
            "not found";
        }

        public void CreateCourse(CourseDTO course)
        {
            if (course == null) throw new ArgumentException("CreateCourse requires a vaild User Object");
            try
            {
                Random ran = new Random();
                Course dbCourse = new Course();
                dbCourse.Id = course.Id;
                dbCourse.StatusId = course.StatusId;
                dbCourse.TrainerId = course.TrainerId;
                dbCourse.DepartmentId = course.DepartmentId;
                dbCourse.Name = course.Name;
                dbCourse.Duration = course.Duration;
                dbCourse.Description = course.Description;
                dbCourse.CreatedOn = DateTime.Now;
                _context.Courses.Add(dbCourse);
                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

    //     public IEnumerable<User> GetUsersByDepartment(int departmentId)
    //     {
    //         if (departmentId == 0) throw new Exception("GetUsersByDepartment requires a vaild Id not zero");
    //         try
    //         {
    //             return _context.Users.Where(u => (u.DepartmentId != 0 && u.DepartmentId == departmentId)).ToList();
    //         }
    //         catch (System.InvalidOperationException ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //         catch (System.Exception ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //     }

        

    
    //     public void UpdateUser(UserDTO user)
    //     {
    //         if (user == null) throw new ArgumentException("UpdateUser requires a vaild User Object");
    //         try
    //         {
    //             var dbUser = _context.Users.Find(user.Id);
    //             if (dbUser != null)
    //             {
    //                 dbUser.RoleId = user.RoleId;
    //                 dbUser.DepartmentId = user.DepartmentId;
    //                 dbUser.Name = user.Name;
    //                 dbUser.UserName = user.UserName;
    //                 dbUser.Password = user.Password;
    //                 dbUser.Email = user.Email;
    //                 dbUser.UpdatedOn = DateTime.Now;
    //                 if (user.Image != null)
    //                 {
    //                     dbUser.Image = user.Image;
    //                 }
    //                 _context.Update(dbUser);
    //                 _context.SaveChanges();
    //             }
    //         }
    //         catch (System.InvalidOperationException ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //         catch (System.Exception ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //     }
    //     public void DisableUser(UserDTO user)
    //     {
    //         if (user == null) throw new ArgumentException("DisableUser requires a vaild User Object");
    //         try
    //         {
    //             var dbUser = _context.Users.Find(user.Id);
    //             if (dbUser != null)
    //             {

    //                 dbUser.isDisabled = true;
    //                 dbUser.UpdatedOn = DateTime.Now;

    //                 _context.Update(dbUser);
    //                 _context.SaveChanges();
    //             }
    //         }
    //         catch (System.InvalidOperationException ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //         catch (System.Exception ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //     }
    //     public void DeleteUser(UserDTO user)
    //     {
    //         if (user == null) throw new ArgumentException("DeleteUser requires a vaild User Object");
    //         try
    //         {
    //             var dbUser = _context.Users.Find(user.Id);
    //             if (dbUser != null)
    //             {
    //                 if (dbUser.isDisabled == true)
    //                 {
    //                     _context.Remove(dbUser);
    //                     _context.SaveChanges();
    //                 }
    //             }
    //         }
    //         catch (System.InvalidOperationException ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //         catch (System.Exception ex)
    //         {
    //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
    //             _logger.LogTrace(ex.ToString());
    //             throw ex;
    //         }
    //     }

    }
}