using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        public IEnumerable<Review> GetReviewByStatusId(int statusId,AppDbContext dbContext)
        {
            var reviewStatusExists = Validation.ReviewStatusExists(dbContext,statusId);
            if(reviewStatusExists) return dbContext.Reviews.Where(r=>r.StatusId==statusId).Include(r=>r.Reviewer).Include(r=>r.Trainee).Include(r=>r.Status);
            else throw new ArgumentException("Invalid Id");            
        }
        public Review GetReviewById(int reviewId,AppDbContext dbContext)
        {
            var reviewExists = Validation.ReviewExists(dbContext,reviewId);
            if(reviewExists)
            {
                var result = dbContext.Reviews.Where(r=>r.Id==reviewId).Include(r=>r.Reviewer).Include(r=>r.Trainee).FirstOrDefault();
                if(result is not null) return result;
            }
            throw new ArgumentException("Invalid Id"); 
        }
        public Dictionary<string,string> CreateReview(Review review,AppDbContext dbContext)
        {
            if(review is null) throw new ArgumentNullException(nameof(review));
            var validation = Validation.ValidateReview(review,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpReviewDetails(review);
                CreateAndSaveReview(review,dbContext);   
            }
            return validation;
        }
        public Dictionary<string,string> UpdateReview(Review review,AppDbContext dbContext)
        {
            if(review is null) throw new ArgumentNullException(nameof(review));
            var validation = Validation.ValidateReview(review,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbReview = dbContext.Reviews.Find(review.Id);
                if(dbReview is not null)
                {
                    SetUpReviewDetails(review, dbReview);
                    UpdateAndSaveReview(dbReview,dbContext);
                }
            }
            return validation;
        }
    }
}