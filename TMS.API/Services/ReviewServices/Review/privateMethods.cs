using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private static void SetUpReviewDetails(Review review, int createdBy)
        {
            review.CreatedOn = DateTime.Now;
            review.CreatedBy = createdBy;
        }
        private static void SetUpReviewDetails(Review review, Review dbReview, int updatedBy)
        {
            if (dbReview.Mode != review.Mode)
                dbReview.Mode = review.Mode;

            if (dbReview.DepartmentId != review.DepartmentId)
                dbReview.DepartmentId = review.DepartmentId;

            if (dbReview.ReviewerId != review.ReviewerId)
                dbReview.ReviewerId = review.ReviewerId;

            if (dbReview.TraineeId != review.TraineeId)
                dbReview.TraineeId = review.TraineeId;

            if (dbReview.ReviewDate != review.ReviewDate)
                dbReview.ReviewDate = review.ReviewDate;

            if (dbReview.ReviewTime != review.ReviewTime)
                dbReview.ReviewTime = review.ReviewTime;
                
            if (dbReview.StatusId != review.StatusId)
                dbReview.StatusId = review.StatusId;

            dbReview.UpdatedOn = DateTime.Now;
            dbReview.UpdatedBy = updatedBy;
        }
    }
}