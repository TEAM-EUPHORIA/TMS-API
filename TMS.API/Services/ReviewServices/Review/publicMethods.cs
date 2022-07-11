using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        public IEnumerable<Review> GetReviewByStatusId(int statusId)
        {
            try
            {
                var reviewStatusExists = _repo.Validation.ReviewStatusExists(statusId);
                if (reviewStatusExists) return _repo.Reviews.GetReviewByStatusId(statusId);
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(GetReviewByStatusId));
                throw;
            }
        }
        public Review GetReviewById(int reviewId)
        {
            try
            {
                var reviewExists = _repo.Validation.ReviewExists(reviewId);
                if (reviewExists)
                {
                    var result = _repo.Reviews.GetReviewById(reviewId);
                    return result;
                }
                throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(GetReviewById));
                throw;
            }
        }
        public Dictionary<string, string> CreateReview(Review review, int createdBy)
        {
            try
            {
                if (review is null) throw new ArgumentNullException(nameof(review));
                var validation = _repo.Validation.ValidateReview(review);
                if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
                {
                    SetUpReviewDetails(review, createdBy);
                    _repo.Reviews.CreateReview(review);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(CreateReview));
                throw;
            }
        }
        public Dictionary<string, string> UpdateReview(Review review, int updatedBy)
        {
            try
            {
                if (review is null) throw new ArgumentNullException(nameof(review));
                var validation = _repo.Validation.ValidateReview(review);
                if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
                {
                    var dbReview = _repo.Reviews.GetReviewById(review.Id);
                    SetUpReviewDetails(review, dbReview, updatedBy);
                    _repo.Reviews.UpdateReview(dbReview);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(UpdateReview));
                throw;
            }
        }
    }
}