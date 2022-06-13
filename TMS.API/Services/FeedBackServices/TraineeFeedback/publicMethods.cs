using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService 
    {
        public TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId)
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
        public Dictionary<string, string> CreateTraineeFeedback(TraineeFeedback traineeFeedback)
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
        public Dictionary<string, string> UpdateTraineeFeedback(TraineeFeedback traineeFeedback)
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
    }
}