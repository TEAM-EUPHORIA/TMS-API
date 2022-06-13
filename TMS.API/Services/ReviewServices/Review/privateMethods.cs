using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private void SetUpReviewDetails(Review review)
        {
            review.CreatedOn = DateTime.Now;
        }
        private void SetUpReviewDetails(Review review, Review dbReview)
        {
            dbReview.Mode = review.Mode;
            dbReview.ReviewerId = review.ReviewerId;
            dbReview.TraineeId = review.TraineeId;
            dbReview.ReviewDate = review.ReviewDate;
            dbReview.ReviewTime = review.ReviewTime;
            dbReview.StatusId = review.StatusId;
            dbReview.UpdatedOn = DateTime.Now;
        }
    }
}