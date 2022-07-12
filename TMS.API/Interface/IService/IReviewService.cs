using TMS.BAL;

namespace TMS.API.Services
{
    public interface IReviewService
    {
        Dictionary<string, string> CreateMom(MOM mom, int createdBy);
        Dictionary<string, string> CreateReview(Review review, int createdBy);
        IEnumerable<MOM> GetListOfMomByUserId(int userId);
        MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId);
        Review GetReviewById(int reviewId);
        IEnumerable<Review> GetReviewByStatusId(int statusId);
        Dictionary<string, string> UpdateMom(MOM mom, int updatedBy);
        Dictionary<string, string> UpdateReview(Review review, int updatedBy);
    }
}