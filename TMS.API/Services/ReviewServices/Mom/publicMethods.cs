using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService : IReviewService
    {
        private readonly UnitOfWork _repo;


        public ReviewService(UnitOfWork repo)
        {
            _repo = repo;

        }
        public IEnumerable<MOM> GetListOfMomByUserId(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists) return _repo.Reviews.GetListOfMomByUserId(userId);
            else throw new ArgumentException("Invalid Id");
        }
        public MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
        {
            var reviewExists = _repo.Validation.ReviewExists(reviewId);
            var traineeExists = _repo.Validation.UserExists(traineeId);
            if (reviewExists && traineeExists)
            {
                var momExists = _repo.Validation.MOMExists(reviewId, traineeId);
                if (momExists)
                {
                    return _repo.Reviews.GetMomByReviewIdAndTraineeId(reviewId, traineeId); ;
                }
            }
            throw new ArgumentException("Inavlid Id's");
        }
        public Dictionary<string, string> CreateMom(MOM mom)
        {
            if (mom is null) throw new ArgumentNullException(nameof(mom));
            var validation = _repo.Validation.ValidateMOM(mom);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpMomDetails(mom);
                _repo.Reviews.CreateMom(mom);
                _repo.Complete();
            }
            return validation;
        }
        public Dictionary<string, string> UpdateMom(MOM mom)
        {
            if (mom is null) throw new ArgumentNullException(nameof(mom));
            var validation = _repo.Validation.ValidateMOM(mom);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbMom = _repo.Reviews.GetMomByReviewIdAndTraineeId(mom.ReviewId, mom.TraineeId);
                SetUpMomDetails(mom, dbMom);
                _repo.Reviews.UpdateMom(dbMom);
                _repo.Complete();
            }
            return validation;
        }
    }
}