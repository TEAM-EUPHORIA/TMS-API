using TMS.BAL;

namespace TMS.API.Services
{
    public interface IReviewService
    {
        Dictionary<string, string> CreateMom(Mom mom);
        Dictionary<string, string> CreateReview(Review review);
        IEnumerable<Mom> GetListOfMomByUserId(int userId);
        Mom GetMomByReviewIdAndTraineeId(int reviewId, int traineeId);
        Review GetReviewById(int reviewId);
        IEnumerable<Review> GetReviewByStatusId(int statusId);
        Dictionary<string, string> UpdateMom(Mom mom);
        Dictionary<string, string> UpdateReview(Review review);
    }
}