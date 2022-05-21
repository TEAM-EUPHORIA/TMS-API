using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseFeedback> _logger;

        public FeedbackService(AppDbContext context, ILogger<CourseFeedback> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public CourseFeedback GetCourseFeedbackByCourseIdAndOwnerId(int courseId, int ownerId)
        {
            if (courseId == 0 || ownerId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetCourseFeedbackByCourseIdAndOwnerId));
            try
            {

                return _context.CourseFeedbacks.Where(cf => cf.CourseId == courseId && cf.OwnerId == ownerId).Include(cf => cf.Course).ThenInclude(c => c.Users).FirstOrDefault();

            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(FeedbackService), nameof(GetCourseFeedbackByCourseIdAndOwnerId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(GetCourseFeedbackByCourseIdAndOwnerId));
                throw;
            }
        }
        public Dictionary<string,string> CreateCourseFeedback(CourseFeedback courseFeedback)
        {
            if (courseFeedback == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateCourseFeedback), nameof(courseFeedback));
            var validation = Validation.ValidateCourseFeedback(courseFeedback,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    CreateAndSaveCourseFeedback(courseFeedback);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(FeedbackService), nameof(CreateCourseFeedback));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(CreateCourseFeedback));
                    throw;
                }
            }
            return validation;
        }
        public Dictionary<string,string> UpdateCourseFeedback(CourseFeedback courseFeedback)
        {
            if (courseFeedback == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateCourseFeedback), nameof(courseFeedback));
            var validation = Validation.ValidateCourseFeedback(courseFeedback,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbCourseFeedback = _context.CourseFeedbacks.Where(c=>c.Id==courseFeedback.Id).FirstOrDefault();
                    if (dbCourseFeedback != null)
                    {
                        SetUpCourseFeedbackDetails(courseFeedback, dbCourseFeedback);
                        UpdateAndSaveCourseFeedback(dbCourseFeedback);
                    }
                    validation.Add("Invalid Id","Not Found");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(FeedbackService), nameof(UpdateCourseFeedback));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(UpdateCourseFeedback));
                    throw;
                }
            }
            return validation;
        }
        private void UpdateAndSaveCourseFeedback(CourseFeedback courseFeedback)
        {
            _context.CourseFeedbacks.Add(courseFeedback);
            _context.SaveChanges();
        }
        private void CreateAndSaveCourseFeedback(CourseFeedback courseFeedback)
        {
            courseFeedback.CreatedOn = DateTime.UtcNow;
            _context.CourseFeedbacks.Add(courseFeedback);
            _context.SaveChanges();
        }
        private void SetUpCourseFeedbackDetails(CourseFeedback courseFeedback,CourseFeedback dbCourseFeedback)
        {
            dbCourseFeedback.Feedback = courseFeedback.Feedback;
            dbCourseFeedback.Rating = courseFeedback.Rating;
            dbCourseFeedback.UpdatedOn = DateTime.UtcNow;
        }
    }
}