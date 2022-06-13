using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        public IEnumerable<Review> GetReviewByStatusId(int statusId)
        {
            var reviewStatusExists = _repo.Validation.ReviewStatusExists(statusId);
            if (reviewStatusExists) return _repo.Reviews.GetReviewByStatusId(statusId);
            else throw new ArgumentException("Invalid Id");
        }
        public Review GetReviewById(int reviewId)
        {
            var reviewExists = _repo.Validation.ReviewExists(reviewId);
            if (reviewExists)
            {
                var result = _repo.Reviews.GetReviewById(reviewId);
                return result;
            }
            throw new ArgumentException("Invalid Id");
        }
        public Dictionary<string, string> CreateReview(Review review)
        {
            if (review is null) throw new ArgumentNullException(nameof(review));
            var validation = _repo.Validation.ValidateReview(review);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpReviewDetails(review);
                _repo.Reviews.CreateReview(review);
            }
            return validation;
        }
        public Dictionary<string, string> UpdateReview(Review review)
        {
            if (review is null) throw new ArgumentNullException(nameof(review));
            var validation = _repo.Validation.ValidateReview(review);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbReview = _repo.Reviews.GetReviewById(review.Id);                
                SetUpReviewDetails(review, dbReview);
                _repo.Reviews.UpdateReview(dbReview);
            }
            return validation;
        }
    }
}