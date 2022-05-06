using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;

namespace TMS.API.Services
{
    public class TraineeFeedbackService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TraineeFeedback> _logger;

        public TraineeFeedbackService(AppDbContext context, ILogger<TraineeFeedback> logger)
        {
            _context = context;
            _logger = logger;
        }

        public TraineeFeedback GetTraineeFeedbackByID(int cid,int tid)
        {
            if (cid == 0 || tid==0) throw new ArgumentException("GetFeedbackByCourseandUserId requires a vaild Id not zero");
            try
            {
               return _context.traineeFeedbacks.Include(tf=> tf.Course).Include(cf=>cf.Trainee).FirstOrDefault(tf=>tf.TraineeId==tid && tf.CourseId==cid);
                
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

        public void CreateTFeedback(TraineeFeedbackDTO traineeFeedback)
        {
            if (traineeFeedback == null) throw new ArgumentException("CreateUser requires a vaild User Object");
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
                TraineeFeedback dbcoursefeedback=new TraineeFeedback();
                dbcoursefeedback.TraineeId=traineeFeedback.TraineeId;
                dbcoursefeedback.TrainerId=traineeFeedback.TrainerId;
                dbcoursefeedback.Feedback=traineeFeedback.Feedback;
               
                 _context.traineeFeedbacks.Add(dbcoursefeedback);
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
        public void UpdateTFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null) throw new ArgumentException("UpdateUser requires a vaild User Object");
            try
            {
                var dbUser = _context.traineeFeedbacks.Find(traineeFeedback.Id);
                if (dbUser != null)
                {
                    dbUser.TraineeId = traineeFeedback.TraineeId;
                    dbUser.TrainerId = traineeFeedback.TrainerId;
                    dbUser.Feedback = traineeFeedback.Feedback;
                                       
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