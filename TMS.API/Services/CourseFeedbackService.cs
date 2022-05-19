using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class CourseFeedbackService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseFeedback> _logger;

        public CourseFeedbackService(AppDbContext context, ILogger<CourseFeedback> logger)
        {
           _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public CourseFeedback GetFeedbackByID(int cid, int oid)
        {
            if (cid == 0 || oid == 0) throw new ArgumentException("GetFeedbackByCourseandUserId requires a vaild Id not zero");
            try
            {
                
                    return _context.CourseFeedbacks.Where(cf => cf.CourseId == cid && cf.OwnerId == oid).Include(cf=>cf.Course).ThenInclude(c=>c.Users).FirstOrDefault();
              
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseFeedbackService), nameof(GetFeedbackByID));
                throw ex;
            }
            catch (Exception ex)
            {
                TMSLogger.EfCoreExceptions(ex, _logger, nameof(CourseFeedbackService), nameof(GetFeedbackByID));
                throw ex;
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseFeedback"></param>
        public bool CreateCFeedback(CourseFeedback courseFeedback)
        {
            if (courseFeedback == null) throw new ArgumentException("CreateFeedback requires a vaild Object");

            try
            {               
                var cl = _context.CourseFeedbacks.ToList();
                var res = cl.Where(u=>u.CourseId==courseFeedback.CourseId&&u.OwnerId==courseFeedback.OwnerId).Count();
               if(res==0){
                   courseFeedback.CreatedOn=DateTime.Now;
                   _context.CourseFeedbacks.Add(courseFeedback);
                   _context.SaveChanges();
                   return true;

               }
               else{
                   return false;
               }         
                 

            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseFeedbackService), nameof(CreateCFeedback));
                throw ex;
            }
            catch (Exception ex)
            {
                TMSLogger.EfCoreExceptions(ex, _logger, nameof(CourseFeedbackService), nameof(CreateCFeedback));
                throw ex;
            }
        }
        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="courseFeedback"></param>
        // public bool UpdateCFeedback(CourseFeedback courseFeedback)
        // {
        //     if (courseFeedback == null) throw new ArgumentException("UpdateFeedback requires a vaild  Object");
        //     try
        //     {
        //         var dbUser = _context.CourseFeedbacks.Find(courseFeedback.Id);
        //         if (dbUser != null)
        //         {
        //             dbUser.CourseId = courseFeedback.CourseId;
        //             dbUser.OwnerId = courseFeedback.OwnerId;
        //             dbUser.Feedback = courseFeedback.Feedback;
        //             dbUser.Rating = courseFeedback.Rating;
        //             dbUser.UpdatedOn = DateTime.Now;

        //             _context.Update(dbUser);
        //             _context.SaveChanges();
        //             return true;
        //         }
        //         return false;
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }
        

    }
}
// using Microsoft.EntityFrameworkCore;
// using TMS.BAL;

// namespace TMS.API.Services
// {
//     public class CourseFeedbackService
//     {
//         private readonly AppDbContext _context;
//         private readonly ILogger<CourseFeedback> _logger;

//         public CourseFeedbackService(AppDbContext context, ILogger<CourseFeedback> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="cid"></param>
//         /// <param name="oid"></param>
//         /// <returns></returns>
//         public CourseFeedback GetFeedbackByID(int cid, int oid)
//         {
//             if (cid == 0 || oid == 0) throw new ArgumentException("GetFeedbackByCourseandUserId requires a vaild Id not zero");
//             try
//             {
//                 return _context.CourseFeedbacks.Include(cf => cf.Course).Include(cf => cf.Owner).FirstOrDefault(cf => cf.CourseId == cid && cf.OwnerId == oid);

//             }
//             catch (System.InvalidOperationException ex)
//             {
//                 _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
//                 _logger.LogTrace(ex.ToString());
//                 throw ex;
//             }
//             catch (System.Exception ex)
//             {
//                 _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
//                 _logger.LogTrace(ex.ToString());
//                 throw ex;
//             }
//         }

      
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="courseFeedback"></param>
//         public void CreateCFeedback(CourseFeedback courseFeedback)
//         {
//             if (courseFeedback == null) throw new ArgumentException("CreateFeedback requires a vaild Object");
//             try
//             {

//                 Random random = new Random();
//                 CourseFeedback dbcoursefeedback = new CourseFeedback();
//                 dbcoursefeedback.CourseId = courseFeedback.CourseId;
//                 dbcoursefeedback.OwnerId = courseFeedback.OwnerId;
//                 dbcoursefeedback.Feedback = courseFeedback.Feedback;
//                 dbcoursefeedback.Rating = courseFeedback.Rating;
//                 _context.CourseFeedbacks.Add(dbcoursefeedback);
//                 _context.SaveChanges();


//             }
//             catch (System.InvalidOperationException ex)
//             {
//                 _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
//                 _logger.LogTrace(ex.ToString());
//                 throw ex;
//             }
//             catch (System.Exception ex)
//             {
//                 _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
//                 _logger.LogTrace(ex.ToString());
//                 throw ex;
//             }
//         }
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="courseFeedback"></param>
//         public void UpdateCFeedback(CourseFeedback courseFeedback)
//         {
//             if (courseFeedback == null) throw new ArgumentException("UpdateFeedback requires a vaild  Object");
//             try
//             {
//                 var dbUser = _context.CourseFeedbacks.Find(courseFeedback.Id);
//                 if (dbUser != null)
//                 {
//                     dbUser.CourseId = courseFeedback.CourseId;
//                     dbUser.OwnerId = courseFeedback.OwnerId;
//                     dbUser.Feedback = courseFeedback.Feedback;
//                     dbUser.Rating = courseFeedback.Rating;
//                     dbUser.UpdatedOn = DateTime.Now;

//                     _context.Update(dbUser);
//                     _context.SaveChanges();
//                 }
//             }
//             catch (System.InvalidOperationException ex)
//             {
//                 _logger.LogCritical("An Critical error occured in Feedback services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
//                 _logger.LogTrace(ex.ToString());
//                 throw ex;
//             }
//             catch (System.Exception ex)
//             {
//                 _logger.LogCritical("An Critical error occured in Feedback services. Some external factors are involved. please check the log files to know more about it");
//                 _logger.LogTrace(ex.ToString());
//                 throw ex;
//             }
//         }
       

//     }
// }