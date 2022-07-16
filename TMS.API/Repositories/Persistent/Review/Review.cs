using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class ReviewRepository : IReviewRepository
    {
        public void CreateReview(Review review)
        {
            dbContext.Reviews.Add(review);
        }
        public void UpdateReview(Review review)
        {
            dbContext.Reviews.Update(review);
        }
        public Review GetReviewById(int reviewId)
        {
            return dbContext.Reviews
                    .Where(r => r.Id == reviewId)
                    .Include(r => r.Reviewer)
                    .Include(r => r.Trainee)
                    .FirstOrDefault()!;
        }
        public IEnumerable<Review> GetReviewByStatusId(int statusId)
        {
            return dbContext.Reviews
                    .WhereIf(statusId == 1, r => r.StatusId == statusId && (r.ReviewDate.Date > DateTime.Now.Date || r.ReviewDate.Date > DateTime.Now.AddDays(-3)))
                    .WhereIf(statusId != 1, r => r.StatusId == statusId)
                    .Include(r => r.Status)
                    .Include(r => r.Reviewer)
                    .Include(r => r.Trainee)
                    .Include(r => r.Mom);
        }
        public IEnumerable<Review> GetReviewByStatusId(int statusId, int userId)
        {
            return dbContext.Reviews
                    .WhereIf(statusId == 1, r => r.StatusId == statusId && 
                            (r.ReviewDate.Date > DateTime.Now.Date || r.ReviewDate.Date > DateTime.Now.AddDays(-3)) && 
                            (r.ReviewerId == userId || r.TraineeId == userId)) 
                    .WhereIf(statusId != 1, r => r.StatusId == statusId && (r.ReviewerId == userId || r.TraineeId == userId))
                    .Include(r => r.Status)
                    .Include(r => r.Reviewer)
                    .Include(r => r.Trainee)
                    .Include(r => r.Mom);
        }
    }
}