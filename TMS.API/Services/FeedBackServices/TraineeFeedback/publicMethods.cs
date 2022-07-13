using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService 
    {
        /// <summary>
        /// used to get single user by user id
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <param name="trainerId"></param>
        /// <returns>
        /// user if user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId)
        {
            try
            {
            var courseExists = _repo.Validation.CourseExists(courseId);
            var traineeExists = _repo.Validation.UserExists(traineeId);
            var trainerExists = _repo.Validation.UserExists(trainerId);
            if (courseExists && traineeExists && trainerExists)
            {
                var traineeFeedbackExists = _repo.Validation.TraineeFeedbackExists(courseId, traineeId, trainerId);
                if (traineeFeedbackExists)
                {
                    return _repo.Feedbacks.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId,traineeId,trainerId);
                }
            }
            throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedbackService), nameof(GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId));
                throw;
            }
        }
        /// <summary>
        /// used to create a Trainee Feedback.
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Dictionary<string, string> CreateTraineeFeedback(TraineeFeedback traineeFeedback, int createdBy)
        {
            try
            {
            if (traineeFeedback is null) throw new ArgumentNullException(nameof(traineeFeedback));
            var validation = _repo.Validation.ValidateTraineeFeedback(traineeFeedback);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpTraineeFeedbackDetails(traineeFeedback);
                _repo.Feedbacks.CreateTraineeFeedback(traineeFeedback);
                _repo.Complete();
            }
            return validation;
            }

            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedbackService), nameof(CreateTraineeFeedback));
                throw;
            }
        }

        /// <summary>
        /// used to update a Trainee Feedback.
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> UpdateTraineeFeedback(TraineeFeedback traineeFeedback, int updatedBy)
        {
            try
            {
            if (traineeFeedback is null) throw new ArgumentNullException(nameof(traineeFeedback));
            var validation = _repo.Validation.ValidateTraineeFeedback(traineeFeedback);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbTraineeFeedback = _repo.Feedbacks.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(traineeFeedback.CourseId,traineeFeedback.TraineeId,traineeFeedback.TrainerId);
                SetUpTraineeFeedbackDetails(traineeFeedback, dbTraineeFeedback);
                _repo.Feedbacks.UpdateTraineeFeedback(dbTraineeFeedback);
                _repo.Complete();
            }
            return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedbackService ), nameof(UpdateTraineeFeedback));
                throw;
            }

        }
    }
}