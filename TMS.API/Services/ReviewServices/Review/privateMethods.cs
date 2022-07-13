using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        /// <summary>
        /// used to SetUpReviewDetails to user.
        /// </summary>
        /// <param name="review"></param>
        /// <param name="createdBy"></param>
        private static void SetUpReviewDetails(Review review, int createdBy)
        {
            review.CreatedOn = DateTime.Now;
            review.CreatedBy = createdBy;
        }

        /// <summary>
        /// used to setup the user ReviewDetails.
        /// </summary>
        /// <param name="review"></param>
        /// <param name="dbReview"></param>
        /// <param name="updatedBy"></param>
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