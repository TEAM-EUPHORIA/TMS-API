using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        /// <summary>
        /// used to  SetUpTraineeFeedbackDetails to user.
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback is null)
            {
                throw new ArgumentException(nameof(traineeFeedback));
            }

            traineeFeedback.CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// used to  SetUpTraineeFeedbackDetails to user.
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <param name="dbTraineeFeedback"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback, TraineeFeedback dbTraineeFeedback)
        {
            if (traineeFeedback is null)
            {
                throw new ArgumentException(nameof(traineeFeedback));
            }

            if (dbTraineeFeedback is null)
            {
                throw new ArgumentException(nameof(dbTraineeFeedback));
            }

            dbTraineeFeedback.Feedback = traineeFeedback.Feedback;
            dbTraineeFeedback.UpdatedOn = DateTime.Now;
        }

    }
}