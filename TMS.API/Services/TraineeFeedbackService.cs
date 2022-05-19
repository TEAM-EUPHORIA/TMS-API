using Microsoft.EntityFrameworkCore;
using TMS.BAL;

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
        /// <summary>
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public TraineeFeedback GetTraineeFeedbackByID(int cid, int tid)
        {
            if (cid == 0 || tid == 0) throw new ArgumentException("GetFeedbackByCourseandUserId requires a vaild Id not zero");
            try
            {
                return _context.TraineeFeedbacks.Include(tf => tf.Course).Include(cf => cf.Trainee).FirstOrDefault(tf => tf.TraineeId == tid && tf.CourseId == cid);

            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
               throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
               throw;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="traineeFeedback"></param>
        public bool CreateTFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null) throw new ArgumentException("CreateFeedback requires a vaild User Object");
            try
            {
                
                _context.TraineeFeedbacks.Add(traineeFeedback);
                _context.SaveChanges();
                return true;
                


            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
               throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
               throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="traineeFeedback"></param>
        public void UpdateTFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null) throw new ArgumentException("UpdateFeedback requires a vaild User Object");
            try
            {
                var dbUser = _context.TraineeFeedbacks.Find(traineeFeedback.Id);
                if (dbUser != null)
                {
                    dbUser.TraineeId = traineeFeedback.TraineeId;
                    dbUser.CourseId = traineeFeedback.CourseId;
                    dbUser.TrainerId = traineeFeedback.TrainerId;
                    dbUser.Feedback = traineeFeedback.Feedback;

                    dbUser.UpdatedOn = DateTime.Now;

                    _context.Update(dbUser);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
               throw;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
               throw;
            }
        }
      

    }
}