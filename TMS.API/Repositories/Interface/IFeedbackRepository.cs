using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IFeedbackRepository
    {
        CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId, AppDbContext dbContext);
        void CreateCourseFeedback(CourseFeedback courseFeedback, AppDbContext dbContext);
        void UpdateCourseFeedback(CourseFeedback courseFeedback, AppDbContext dbContext);
        TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId, AppDbContext dbContext);
        void CreateTraineeFeedback(TraineeFeedback traineeFeedback, AppDbContext dbContext);
        void UpdateTraineeFeedback(TraineeFeedback traineeFeedback, AppDbContext dbContext);
    }
}