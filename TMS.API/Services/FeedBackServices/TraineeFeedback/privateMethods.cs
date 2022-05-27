using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        private void UpdateAndSaveTraineeFeedback(TraineeFeedback traineeFeedback,AppDbContext dbContext)
        {
            dbContext.TraineeFeedbacks.Update(traineeFeedback);
            dbContext.SaveChanges();
        }
        private void CreateAndSaveTraineeFeedback(TraineeFeedback traineeFeedback,AppDbContext dbContext)
        {
            traineeFeedback.CreatedOn = DateTime.UtcNow;
            dbContext.TraineeFeedbacks.Add(traineeFeedback);
            dbContext.SaveChanges();
        }
        private void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback,TraineeFeedback dbTraineeFeedback)
        {
            dbTraineeFeedback.Feedback = traineeFeedback.Feedback;
            dbTraineeFeedback.UpdatedOn = DateTime.UtcNow;
        }    

    }
}