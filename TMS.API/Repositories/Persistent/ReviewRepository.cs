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

        public void CreateMom(MOM mom)
        {
            dbContext.MOMs.Add(mom);
        }

        public void CreateReview(Review review)
        {
            dbContext.Reviews.Add(review);
        }

        public IEnumerable<MOM> GetListOfMomByUserId(int userId)
        {
            return dbContext.MOMs.Where(m=>m.TraineeId == userId);
        }

        public MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
        {
            return dbContext.MOMs.Where(m=>m.ReviewId == reviewId && m.TraineeId == traineeId).FirstOrDefault();
        }

        public Review GetReviewById(int reviewId)
        {
            return dbContext.Reviews.Where(r=>r.Id == reviewId).FirstOrDefault();
        }

        public IEnumerable<Review> GetReviewByStatusId(int statusId)
        {
            return dbContext.Reviews.Where(r=>r.StatusId == statusId);
        }

        public void UpdateMom(MOM mom)
        {
            dbContext.MOMs.Update(mom);
        }

        public void UpdateReview(Review review)
        {
            dbContext.Reviews.Update(review);
        }
    }
}