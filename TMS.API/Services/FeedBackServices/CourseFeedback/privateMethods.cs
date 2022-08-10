using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        /// <summary>
        /// used to SetUpCourseFeedbackDetails to user.
        /// </summary>
        /// <param name="courseFeedback"></param>
        private static void SetUpCourseFeedbackDetails(CourseFeedback courseFeedback)
        {
            if (courseFeedback is null)
            {
                throw new ArgumentException(nameof(courseFeedback));
            }

            courseFeedback.CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// used to SetUpCourseFeedbackDetails to user.
        /// </summary>
        /// <param name="courseFeedback"></param>
        /// <param name="dbCourseFeedback"></param>
    
        private static void SetUpCourseFeedbackDetails(CourseFeedback courseFeedback,CourseFeedback dbCourseFeedback)
        {
            if (courseFeedback is null)
            {
                throw new ArgumentException(nameof(courseFeedback));
            }

            if (dbCourseFeedback is null)
            {
                throw new ArgumentException(nameof(dbCourseFeedback));
            }

            dbCourseFeedback.Feedback = courseFeedback.Feedback;
            dbCourseFeedback.Rating = courseFeedback.Rating;
            dbCourseFeedback.UpdatedOn = DateTime.Now;
        }
    }
}