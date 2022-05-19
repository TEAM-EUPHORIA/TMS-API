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
    }
}