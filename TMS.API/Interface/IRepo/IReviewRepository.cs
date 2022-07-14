using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewByStatusId(int statusId);
        IEnumerable<Review> GetReviewByStatusId(int statusId,int userId);
        Review GetReviewById(int reviewId);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void CreateMom(MOM mom);
        void UpdateMom(MOM mom);
        IEnumerable<MOM> GetListOfMomByUserId(int userId);
        MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId);
    }
}