using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of FeedbackService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public FeedbackService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        /// used to get single user by user id
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <returns>
        /// user if user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>


        public CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId)
        {
            try
            {
                var courseExists = _repo.Validation.CourseExists(courseId);
                var traineeExists = _repo.Validation.UserExists(traineeId);
                if (courseExists && traineeExists)
                {
                    return _repo.Feedbacks.GetCourseFeedbackByCourseIdAndTraineeId(courseId, traineeId);
                }
                else throw new ArgumentException("Invalid Id's");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedbackService), nameof(GetCourseFeedbackByCourseIdAndTraineeId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetCourseFeedbackByCourseIdAndTraineeId));
                throw;
            }
        }

        /// <summary>
        /// used to create a course feedback to user.
        /// </summary>
        /// <param name="courseFeedback"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        ///  result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Dictionary<string, string> CreateCourseFeedback(CourseFeedback courseFeedback, int createdBy)
        {
            try
            {
                if (courseFeedback is null) throw new ArgumentNullException(nameof(courseFeedback));
                var validation = _repo.Validation.ValidateCourseFeedback(courseFeedback);
                if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
                {
                    SetUpCourseFeedbackDetails(courseFeedback);
                    _repo.Feedbacks.CreateCourseFeedback(courseFeedback);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedbackService), nameof(CreateCourseFeedback));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CreateCourseFeedback));
                throw;
            }

        }

        /// <summary>
        /// used to Update a course feedback to user.
        /// </summary>
        /// <param name="courseFeedback"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        ///  result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>

        public Dictionary<string, string> UpdateCourseFeedback(CourseFeedback courseFeedback, int updatedBy)
        {
            try
            {
                if (courseFeedback is null) throw new ArgumentNullException(nameof(courseFeedback));
                var validation = _repo.Validation.ValidateCourseFeedback(courseFeedback);
                if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
                {
                    var dbCourseFeedback = _repo.Feedbacks.GetCourseFeedbackByCourseIdAndTraineeId(courseFeedback.CourseId, courseFeedback.TraineeId);
                    SetUpCourseFeedbackDetails(courseFeedback, dbCourseFeedback);
                    _repo.Feedbacks.UpdateCourseFeedback(dbCourseFeedback);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedbackService), nameof(UpdateCourseFeedback));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UpdateCourseFeedback));
                throw;
            }
        }
    }
}