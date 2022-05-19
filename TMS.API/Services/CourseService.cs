using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.BAL;


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
                return _context.Topics.Where(t => t.CourseId == id).Include(t => t.Course).ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw;
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
                throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw;
            }
        }
        public Object GetCourseById(int id)
        {
            if (id == 0) throw new ArgumentException("GetFeedbackByCourseandUserId requires a vaild Id not zero");
            var obj = _context.Courses.Where(c => c.Id == id).ToList();
            if (obj != null)
            {
                return obj;
            }
            return
            "not found";
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
                    Context = dbTopic.Content
                };

                return result;
            }
            return "not found";
        }

        public void CreateCourse(Course course)
        {
            if (course == null) throw new ArgumentException("CreateCourse requires a vaild User Object");
            try
            {
                var user = _context.Users.Find(course.TrainerId);
                course.Users =new ();
                course.Users.Add(user);

                course.CreatedOn = DateTime.Now;
                _context.Courses.Add(course);
                _context.SaveChanges();
            
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in Course services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in Course services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw;
            }
        }

        public void UpdateCourse(Course course)
        {
            if (course == null) throw new ArgumentException("UpdateCourse requires a vaild User Object");
            try
            {
                var dbCourse = _context.Courses.Find(course.Id);
                if (dbCourse != null)
                    dbCourse.Id = course.Id;
                dbCourse.StatusId = course.StatusId;
                // dbCourse.TrainerId = course.TrainerId;
                dbCourse.DepartmentId = course.DepartmentId;
                dbCourse.Name = course.Name;
                dbCourse.Duration = course.Duration;
                dbCourse.Description = course.Description;
                _context.Update(dbCourse);
                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw;
            }
        }

        public void DisableCourse(int courseId)
        {
            if (courseId == 0) throw new ArgumentException("DisableCourse requires a vaild User Object");
            try
            {
                var dbCourse = _context.Courses.Find(courseId);
                if (dbCourse != null)
                {

                    dbCourse.isDisabled = true;
                    dbCourse.UpdatedOn = DateTime.Now;

                    _context.Update(dbCourse);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw;
            }
        }


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
        //            throw;
        //         }
        //         catch (System.Exception ex)
        //         {
        //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //             _logger.LogTrace(ex.ToString());
        //            throw;
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
        //            throw;
        //         }
        //         catch (System.Exception ex)
        //         {
        //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //             _logger.LogTrace(ex.ToString());
        //            throw;
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
        //            throw;
        //         }
        //         catch (System.Exception ex)
        //         {
        //             _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //             _logger.LogTrace(ex.ToString());
        //            throw;
        //         }
        //     }

    }
}




