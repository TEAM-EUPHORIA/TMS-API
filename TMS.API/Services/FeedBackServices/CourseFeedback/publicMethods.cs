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
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        /// <summary>
        /// used to get single user by user id
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <returns>
        /// user if user is found
        /// </returns>
        public CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            var traineeExists = _repo.Validation.UserExists(traineeId);
            if (courseExists && traineeExists)
            {
                return _repo.Feedbacks.GetCourseFeedbackByCourseIdAndTraineeId(courseId, traineeId);
            }
            else throw new ArgumentException("Invalid Id's");
        }
        /// <summary>
        /// used to create a course feedback to user.
        /// </summary>
        /// <param name="courseFeedback"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        ///  result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> CreateCourseFeedback(CourseFeedback courseFeedback, int createdBy)
        {
            if (courseFeedback is null) throw new ArgumentException(nameof(courseFeedback));
            var validation = _repo.Validation.ValidateCourseFeedback(courseFeedback);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpCourseFeedbackDetails(courseFeedback);
                _repo.Feedbacks.CreateCourseFeedback(courseFeedback);
                _repo.Complete();
            }
            return validation;
        }
        /// <summary>
        /// used to Update a course feedback to user.
        /// </summary>
        /// <param name="courseFeedback"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        ///  result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> UpdateCourseFeedback(CourseFeedback courseFeedback, int updatedBy)
        {
            if (courseFeedback is null) throw new ArgumentException(nameof(courseFeedback));
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
    }
}