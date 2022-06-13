using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        private void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback)
        {
            traineeFeedback.CreatedOn = DateTime.UtcNow;
        }
        private void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback,TraineeFeedback dbTraineeFeedback)
        {
            dbTraineeFeedback.Feedback = traineeFeedback.Feedback;
            dbTraineeFeedback.UpdatedOn = DateTime.UtcNow;
        }    

    }
}