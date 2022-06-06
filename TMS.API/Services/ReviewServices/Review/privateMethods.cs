using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private void CreateAndSaveReview(Review review,AppDbContext dbContext)
        {
            //review.StatusId=1;
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();
        }
        private void SetUpReviewDetails(Review review)
        {
            review.CreatedOn = DateTime.UtcNow;
        }
        private void UpdateAndSaveReview(Review dbReview,AppDbContext dbContext)
        {
            dbContext.Reviews.Update(dbReview);
            dbContext.SaveChanges();
        }
        private void SetUpReviewDetails(Review review, Review dbReview)
        {
            dbReview.Mode = review.Mode;
            dbReview.ReviewerId = review.ReviewerId;
            dbReview.TraineeId = review.TraineeId;
            dbReview.ReviewDate = review.ReviewDate;
            dbReview.ReviewTime = review.ReviewTime;
            dbReview.StatusId = review.StatusId;
            dbReview.UpdatedOn = DateTime.UtcNow;
        }
    }
}