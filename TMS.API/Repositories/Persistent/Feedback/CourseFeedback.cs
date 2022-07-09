using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class FeedbackRepository : IFeedbackRepository
    {
        public void CreateCourseFeedback(CourseFeedback courseFeedback)
        {
            dbContext.CourseFeedbacks.Add(courseFeedback);
        }
        public CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId)
        {
            return dbContext.CourseFeedbacks
                    .Where(cf => cf.CourseId == courseId && cf.TraineeId == traineeId)
                    .FirstOrDefault()!;
        }
        public IEnumerable<CourseFeedback> GetCourseFeedbacksByCourseId(int courseId)
        {
            return dbContext.CourseFeedbacks.Where(cf => cf.CourseId == courseId).Include(cf => cf.Trainee).ToList();
        }
        public void UpdateCourseFeedback(CourseFeedback courseFeedback)
        {
            dbContext.CourseFeedbacks.Update(courseFeedback);
        }
    }
}