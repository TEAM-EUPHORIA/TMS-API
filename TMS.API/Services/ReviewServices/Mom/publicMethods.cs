using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;

        public ReviewService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public IEnumerable<MOM> GetListOfMomByUserId(int userId)
        {
            try
            {
                var userExists = _repo.Validation.UserExists(userId);
                if (userExists) return _repo.Reviews.GetListOfMomByUserId(userId);
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(GetListOfMomByUserId));
                throw;
            }
        }
        public MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
        {
            try
            {
                var reviewExists = _repo.Validation.ReviewExists(reviewId);
                var traineeExists = _repo.Validation.UserExists(traineeId);
                if (reviewExists && traineeExists)
                {
                    var momExists = _repo.Validation.MOMExists(reviewId, traineeId);
                    if (momExists)
                    {
                        return _repo.Reviews.GetMomByReviewIdAndTraineeId(reviewId, traineeId);
                    }
                }
                throw new ArgumentException("Inavlid Id's");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(GetMomByReviewIdAndTraineeId));
                throw;
            }
        }
        public Dictionary<string, string> CreateMom(MOM mom, int createdBy)
        {
            try
            {
                if (mom is null) throw new ArgumentNullException(nameof(mom));
                var validation = _repo.Validation.ValidateMOM(mom);
                if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
                {
                    SetUpMomDetails(mom, createdBy);
                    _repo.Reviews.CreateMom(mom);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(CreateMom));
                throw;
            }
        }
        public Dictionary<string, string> UpdateMom(MOM mom, int updatedBy)
        {
            try
            {
                if (mom is null) throw new ArgumentNullException(nameof(mom));
                var validation = _repo.Validation.ValidateMOM(mom);
                if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
                {
                    var dbMom = _repo.Reviews.GetMomByReviewIdAndTraineeId(mom.ReviewId, mom.TraineeId);
                    SetUpMomDetails(mom, dbMom, updatedBy);
                    _repo.Reviews.UpdateMom(dbMom);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewService), nameof(UpdateMom));
                throw;
            }
        }

        public Dictionary<string, string> CreateMom(MOM mom)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> CreateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> UpdateMom(MOM mom)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}