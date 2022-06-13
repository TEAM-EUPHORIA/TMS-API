using TMS.BAL;

namespace TMS.API.Repositories
{
    class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext dbContext;
        public FeedbackRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateCourseFeedback(CourseFeedback courseFeedback)
        {
            dbContext.CourseFeedbacks.Add(courseFeedback);
        }

        public void CreateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            dbContext.TraineeFeedbacks.Add(traineeFeedback);
        }

        public CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId)
        {
            return dbContext.CourseFeedbacks.Where(cf=> cf.CourseId == courseId && cf.TraineeId == traineeId).FirstOrDefault();
        }

        public TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId)
        {
            return dbContext.TraineeFeedbacks.Where(tf=> tf.CourseId == courseId && tf.TraineeId == traineeId && tf.TrainerId == trainerId).FirstOrDefault();
        }

        public void UpdateCourseFeedback(CourseFeedback courseFeedback)
        {
            dbContext.CourseFeedbacks.Update(courseFeedback);
        }

        public void UpdateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            dbContext.TraineeFeedbacks.Update(traineeFeedback);
        }
    }
}