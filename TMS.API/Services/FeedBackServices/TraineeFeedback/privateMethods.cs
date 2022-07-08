using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        private static void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback)
        {
            traineeFeedback.CreatedOn = DateTime.Now;
        }
        private static void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback,TraineeFeedback dbTraineeFeedback)
        {
            dbTraineeFeedback.Feedback = traineeFeedback.Feedback;
            dbTraineeFeedback.UpdatedOn = DateTime.Now;
        }    

    }
}