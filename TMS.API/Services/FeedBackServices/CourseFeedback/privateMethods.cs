using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        private void UpdateAndSaveCourseFeedback(CourseFeedback courseFeedback,AppDbContext dbContext)
        {
            dbContext.CourseFeedbacks.Update(courseFeedback);
            dbContext.SaveChanges();
        }
        private void CreateAndSaveCourseFeedback(CourseFeedback courseFeedback,AppDbContext dbContext)
        {
            courseFeedback.CreatedOn = DateTime.UtcNow;
            dbContext.CourseFeedbacks.Add(courseFeedback);
            dbContext.SaveChanges();
        }
        private void SetUpCourseFeedbackDetails(CourseFeedback courseFeedback,CourseFeedback dbCourseFeedback)
        {
            dbCourseFeedback.Feedback = courseFeedback.Feedback;
            dbCourseFeedback.Rating = courseFeedback.Rating;
            dbCourseFeedback.UpdatedOn = DateTime.UtcNow;
        }
    }
}