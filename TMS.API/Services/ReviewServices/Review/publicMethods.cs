using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {

        /// <summary>
        /// used to get review  based on status id
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns>
        /// IEnumerable Review if status is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
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
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetReviewByStatusId));
                throw;
            }
        }
        /// <summary>
        /// used to get review  based on status id
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// IEnumerable Review if status is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Review> GetReviewByStatusId(int statusId,int userId)
        {
            try
            {
                var reviewStatusExists = _repo.Validation.ReviewStatusExists(statusId);
                if (reviewStatusExists) return _repo.Reviews.GetReviewByStatusId(statusId,userId);
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(GetReviewByStatusId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetReviewByStatusId));
                throw;
            }
        }

        /// <summary>
        /// used to get review  based on review id
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns>
        /// review if review is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception> 
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
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(GetReviewById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetReviewById));
                throw;
            }
        }

        /// <summary>
        /// used to create Review.
        /// </summary>
        /// <param name="review"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

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
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CreateReview));
                throw;
            }
        }

        /// <summary>
        /// used to Update Review.
        /// </summary>
        /// <param name="review"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
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
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UpdateReview));
                throw;
            }
        }
    }
}