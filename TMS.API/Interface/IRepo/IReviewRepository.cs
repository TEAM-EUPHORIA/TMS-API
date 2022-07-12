using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewByStatusId(int statusId);
        Review GetReviewById(int reviewId);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void CreateMom(Mom mom);
        void UpdateMom(Mom mom);
        IEnumerable<Mom> GetListOfMomByUserId(int userId);
        Mom GetMomByReviewIdAndTraineeId(int reviewId, int traineeId);
    }
}