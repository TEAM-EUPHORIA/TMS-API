using TMS.BAL;

namespace TMS.API.Services
{
    public interface IReviewService
    {
<<<<<<< HEAD
        Dictionary<string, string> CreateMom(MOM mom, int createdBy);
        Dictionary<string, string> CreateReview(Review review, int createdBy);
        IEnumerable<MOM> GetListOfMomByUserId(int userId);
        MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId);
        Review GetReviewById(int reviewId);
        IEnumerable<Review> GetReviewByStatusId(int statusId);
        Dictionary<string, string> UpdateMom(MOM mom, int updatedBy);
        Dictionary<string, string> UpdateReview(Review review, int updatedBy);
=======
        Dictionary<string, string> CreateMom(Mom mom);
        Dictionary<string, string> CreateReview(Review review);
        IEnumerable<Mom> GetListOfMomByUserId(int userId);
        Mom GetMomByReviewIdAndTraineeId(int reviewId, int traineeId);
        Review GetReviewById(int reviewId);
        IEnumerable<Review> GetReviewByStatusId(int statusId);
        Dictionary<string, string> UpdateMom(Mom mom);
        Dictionary<string, string> UpdateReview(Review review);
>>>>>>> 985156ff0e154d4657b256b533b831ed906b2839
    }
}