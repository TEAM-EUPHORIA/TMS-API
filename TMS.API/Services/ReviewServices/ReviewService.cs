using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(AppDbContext context, ILogger<ReviewService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Review> GetReviewByStatusId(int statusId)
        {
            if (statusId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetReviewByStatusId));
            try
            {
                var result = _context.Reviews.Where(r=>r.StatusId==statusId).Include(r=>r.Reviewer).Include(r=>r.Trainee).Include(r=>r.Status);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(GetReviewByStatusId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetReviewByStatusId));
                throw;
            }
        }
        public Review GetReviewById(int reviewId)
        {
            if (reviewId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetReviewById));
            try
            {
                return _context.Reviews.Where(r=>r.Id==reviewId).Include(r=>r.Reviewer).Include(r=>r.Trainee).Include(r=>r.Status).FirstOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(GetReviewById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetReviewById));
                throw;
            }
        }
        public Dictionary<string,string> CreateReview(Review review)
        {
             if (review == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateReview), nameof(review));
            var validation = Validation.ValidateReview(review,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                     SetUpReviewDetails(review);
                    CreateAndSaveReview(review);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(CreateReview));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(CreateReview));
                    throw;
                }
            }
            return validation;
        }
        public Dictionary<string,string> UpdateReview(Review review)
        {
            if (review == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateReview), nameof(review));
            var validation = Validation.ValidateReview(review,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbReview = _context.Reviews.Where(r=>r.Id==review.Id).FirstOrDefault();
                    if (dbReview != null)
                    {
                         SetUpReviewDetails(review, dbReview);
                        UpdateAndSaveReview(dbReview);
                    }
                    else{
                            validation.Add("Invalid Id","Not Found");
                    }
                    
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(UpdateReview));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(UpdateReview));
                    throw;
                }
            }
            return validation;
        }
        public bool DisableReview(int reviewId)
        {
            if (reviewId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableReview));
            try
            {
                var dbReview = _context.Reviews.Find(reviewId);
                if (dbReview != null)
                {
                    dbReview.isDisabled = true;
                    dbReview.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveReview(dbReview);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(DisableReview));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(DisableReview));
                throw;
            }
        }
        private void CreateAndSaveReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
        private void SetUpReviewDetails(Review review)
        {
            review.isDisabled = false;
            review.CreatedOn = DateTime.UtcNow;
        }
        private void UpdateAndSaveReview(Review dbReview)
        {
            _context.Reviews.Update(dbReview);
            _context.SaveChanges();
        }
        private void SetUpReviewDetails(Review review, Review dbReview)
        {
            dbReview.Mode = review.Mode;
            dbReview.ReviewerId = review.ReviewerId;
            dbReview.TraineeId = review.TraineeId;
            // dbReview.ReviewDate = review.ReviewDate;
            // dbReview.ReviewTime = review.ReviewTime;
            dbReview.StatusId = review.StatusId;
        }
    }
}