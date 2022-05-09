using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;

namespace TMS.API.Services
{
    public class CourseFeedbackService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseFeedback> _logger;

        public CourseFeedbackService(AppDbContext context, ILogger<CourseFeedback> logger)
        {
            _context = context;
            _logger = logger;
        }

        public CourseFeedback GetFeedbackByID(int cid,int oid)
        {
            if (cid == 0|| oid==0) throw new ArgumentException("GetFeedbackByCourseandUserId requires a vaild Id not zero");
            try
            {
               return _context.CourseFeedbacks.Include(cf=> cf.Course).Include(cf=>cf.Owner).FirstOrDefault(cf=>cf.CourseId==cid && cf.OwnerId == oid);
                
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

        // public IEnumerable<User> GetUsersByDepartment(int departmentId)
        // {
        //     if (departmentId == 0) throw new Exception("GetUsersByDepartment requires a vaild Id not zero");
        //     try
        //     {
        //         return _context.Users.Where(u => (u.DepartmentId != 0 && u.DepartmentId == departmentId)).ToList();
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }

        public void CreateCFeedback(CourseFeedbackDTO courseFeedback)
        {
            if (courseFeedback == null) throw new ArgumentException("CreateUser requires a vaild User Object");
            try
            {
                // Random ran = new Random();
                // User dbUser = new User();
                // dbUser.RoleId = user.RoleId;
                // dbUser.DepartmentId = user.DepartmentId;
                // dbUser.Name = user.Name;
                // dbUser.UserName = user.UserName;
                // dbUser.Password = user.Password;
                // dbUser.Email = user.Email;
                // dbUser.Image = user.Image;
                // dbUser.EmployeeId = ($"ACE{user.RoleId}{ran.Next(0, 10000)}");
                // dbUser.isDisabled = false;
                // dbUser.CreatedOn = DateTime.Now;
                // _context.SaveChanges();
                Random random=new Random();
                CourseFeedback dbcoursefeedback=new CourseFeedback();
                dbcoursefeedback.CourseId=courseFeedback.CourseId;
                dbcoursefeedback.OwnerId=courseFeedback.OwnerId;
                dbcoursefeedback.Feddback=courseFeedback.Feddback;
                dbcoursefeedback.Rating=courseFeedback.Rating;
                 _context.CourseFeedbacks.Add(dbcoursefeedback);
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
        public void UpdateCFeedback(CourseFeedback courseFeedback)
        {
            if (courseFeedback == null) throw new ArgumentException("UpdateUser requires a vaild User Object");
            try
            {
                var dbUser = _context.CourseFeedbacks.Find(courseFeedback.Id);
                if (dbUser != null)
                {
                    dbUser.CourseId = courseFeedback.CourseId;
                    dbUser.OwnerId = courseFeedback.OwnerId;
                    dbUser.Feddback = courseFeedback.Feddback;
                    dbUser.Rating = courseFeedback.Rating;                   
                    dbUser.UpdatedOn = DateTime.Now;
                    
                    _context.Update(dbUser);
                    _context.SaveChanges();
                }
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
        // public void DisableUser(User user)
        // {
        //     if (user == null) throw new ArgumentException("DisableUser requires a vaild User Object");
        //     try
        //     {
        //         var dbUser = _context.Users.Find(user.Id);
        //         if (dbUser != null)
        //         {

        //             dbUser.isDisabled = true;
        //             dbUser.UpdatedOn = DateTime.Now;

        //             _context.Update(dbUser);
        //             _context.SaveChanges();
        //         }
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }
        // public void DeleteUser(User user)
        // {
        //     if (user == null) throw new ArgumentException("DeleteUser requires a vaild User Object");
        //     try
        //     {
        //         var dbUser = _context.Users.Find(user.Id);
        //         if (dbUser != null)
        //         {
        //             if (dbUser.isDisabled == true)
        //             {
        //                 _context.Remove(dbUser);
        //                 _context.SaveChanges();
        //             }
        //         }
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }

    }
}