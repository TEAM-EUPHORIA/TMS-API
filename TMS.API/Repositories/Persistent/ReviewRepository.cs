using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Repositories
{
    class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext dbContext;
        public ReviewRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateMom(Mom mom)
        {
            var review = dbContext.Reviews.Find(mom.ReviewId);
            review!.StatusId = 2;
            dbContext.MOMs.Add(mom);
        }

        public void CreateReview(Review review)
        {
            dbContext.Reviews.Add(review);
        }

        public IEnumerable<Mom> GetListOfMomByUserId(int userId)
        {
            return dbContext.MOMs
                    .Where(m => m.TraineeId == userId)
                    .Include(m => m.Trainee);
        }

        public Mom GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
        {
            return dbContext.MOMs
                    .Where(m => m.ReviewId == reviewId && m.TraineeId == traineeId)
                    .Include(m => m.Review).ThenInclude(r => r!.Reviewer)
                    .Include(m => m.Trainee)
                    .FirstOrDefault()!;
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
                    .Where(r => r.StatusId == statusId)
                    .Include(r => r.Status).Include(r => r.Reviewer).Include(r => r.Trainee);
        }

        public void UpdateMom(Mom mom)
        {
            dbContext.MOMs.Update(mom);
        }

        public void UpdateReview(Review review)
        {
            dbContext.Reviews.Update(review);
        }
    }
}