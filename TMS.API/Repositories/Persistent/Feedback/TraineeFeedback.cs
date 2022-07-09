using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class FeedbackRepository : IFeedbackRepository
    {
        public void CreateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            dbContext.TraineeFeedbacks.Add(traineeFeedback);
        }
        public TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId)
        {
            return dbContext.TraineeFeedbacks
                    .Where(tf => tf.CourseId == courseId && tf.TraineeId == traineeId && tf.TrainerId == trainerId)
                    .Include(tf => tf.Trainee).Include(tf => tf.Trainer)
                    .FirstOrDefault()!;
        }
        public void UpdateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            dbContext.TraineeFeedbacks.Update(traineeFeedback);
        }
    }
}