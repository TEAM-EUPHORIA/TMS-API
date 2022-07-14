using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        /// <summary>
        /// used to  SetUpTraineeFeedbackDetails to user.
        /// </summary>
        /// <param name="traineeFeedback"></param>

        private static void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback)
        {
            traineeFeedback.CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// used to  SetUpTraineeFeedbackDetails to user.
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <param name="dbTraineeFeedback"></param>
        private static void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback,TraineeFeedback dbTraineeFeedback)
        {
            dbTraineeFeedback.Feedback = traineeFeedback.Feedback;
            dbTraineeFeedback.UpdatedOn = DateTime.Now;
        }    

    }
}