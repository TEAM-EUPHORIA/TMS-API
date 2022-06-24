using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public interface IFeedbackService
    {
        Dictionary<string, string> CreateCourseFeedback(CourseFeedback courseFeedback);
        Dictionary<string, string> CreateTraineeFeedback(TraineeFeedback traineeFeedback);
        CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId);
        TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId);
        Dictionary<string, string> UpdateCourseFeedback(CourseFeedback courseFeedback);
        Dictionary<string, string> UpdateTraineeFeedback(TraineeFeedback traineeFeedback);
    }

    public partial class FeedbackService : IFeedbackService
    {
        private readonly UnitOfWork _repo;


        public FeedbackService(UnitOfWork repo)
        {
            _repo = repo;

        }
        public CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            var traineeExists = _repo.Validation.UserExists(traineeId);
            if (courseExists && traineeExists)
            {
                return _repo.Feedbacks.GetCourseFeedbackByCourseIdAndTraineeId(courseId, traineeId);
            }
            throw new ArgumentException("Invalid Id's");
        }
        public Dictionary<string, string> CreateCourseFeedback(CourseFeedback courseFeedback)
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
        public Dictionary<string, string> UpdateCourseFeedback(CourseFeedback courseFeedback)
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
    }
}