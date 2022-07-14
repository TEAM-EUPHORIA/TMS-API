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
            if(statusId != 1)
            {
                return dbContext.Reviews
                        .Where(r => r.StatusId == statusId)
                        .Include(r => r.Status)
                        .Include(r => r.Reviewer)
                        .Include(r => r.Trainee)
                        .Include(r=>r.Mom);
            }
            else
            {
                return dbContext.Reviews
                        .Where(r => r.StatusId == statusId && r.ReviewTime > DateTime.Now)
                        .Include(r => r.Status)
                        .Include(r => r.Reviewer)
                        .Include(r => r.Mom);

            }
        }
        public IEnumerable<Review> GetReviewByStatusId(int statusId, int userId)
        {
            return dbContext.Reviews
                    .Where(r => r.StatusId == statusId && (r.ReviewerId == userId || r.TraineeId == userId))
                    .Include(r => r.Status)
                    .Include(r => r.Reviewer)
                    .Include(r => r.Trainee)
                    .Include(r=>r.Mom);
        }
    }
}