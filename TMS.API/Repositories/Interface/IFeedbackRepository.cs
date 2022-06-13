using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IFeedbackRepository
    {
        CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId);
        void CreateCourseFeedback(CourseFeedback courseFeedback);
        void UpdateCourseFeedback(CourseFeedback courseFeedback);
        TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId);
        void CreateTraineeFeedback(TraineeFeedback traineeFeedback);
        void UpdateTraineeFeedback(TraineeFeedback traineeFeedback);
    }
}