using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        private void SetUpCourseFeedbackDetails(CourseFeedback courseFeedback)
        {
            courseFeedback.CreatedOn = DateTime.UtcNow;
        }
        private void SetUpCourseFeedbackDetails(CourseFeedback courseFeedback,CourseFeedback dbCourseFeedback)
        {
            dbCourseFeedback.Feedback = courseFeedback.Feedback;
            dbCourseFeedback.Rating = courseFeedback.Rating;
            dbCourseFeedback.UpdatedOn = DateTime.UtcNow;
        }
    }
}