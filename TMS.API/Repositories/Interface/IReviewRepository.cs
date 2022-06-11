using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewByStatusId(int statusId, AppDbContext dbContext);
        Review GetReviewById(int reviewId, AppDbContext dbContext);
        void CreateReview(Review review, AppDbContext dbContext);
        void UpdateReview(Review review, AppDbContext dbContext);
        void CreateMom(MOM mom, AppDbContext dbContext);
        void UpdateMom(MOM mom, AppDbContext dbContext);
        IEnumerable<MOM> GetListOfMomByUserId(int userId, AppDbContext dbContext);
        MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId, AppDbContext dbContext);
    }
}