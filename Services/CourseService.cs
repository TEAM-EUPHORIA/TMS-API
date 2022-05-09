using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;

namespace TMS.API.Services
{
    public class CourseService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseService> _logger;
   
        public CourseService(AppDbContext context, ILogger<CourseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Topic> GetAllTopicsByCourseId(int id)
        {
            
            try
            {
                return _context.Topics.Where(t=>t.CourseId == id).Include(t=>t.Course).ToList();
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
        
        public IEnumerable<Course> GetAllCourses()
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
            var obj = _context.Courses.Where(c=>c.Id==id).FirstOrDefault(c=>c.Id==1 || c.Id==id);
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
        public Object GetTopicDetailsById(int id)
        {
            var dbTopic = _context.Topics.Where(u => u.Id == id).Include("Course").FirstOrDefault();
            if (dbTopic != null)
            {

                var result = new
                {
                    Id = dbTopic.Id,
                    Course = dbTopic.CourseId,
                    Name = dbTopic.Name,
                    Duration = dbTopic.Duration,
                    Context = dbTopic.Context
                };

                return result;
            }
            return "not found";
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
        
    


