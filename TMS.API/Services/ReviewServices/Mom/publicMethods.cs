using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor of ReviewService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ReviewService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        /// <summary>
        /// used to get list of Mom by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// IEnumerable MOM if user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public IEnumerable<MOM> GetListOfMomByUserId(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists) return _repo.Reviews.GetListOfMomByUserId(userId);
            else throw new ArgumentException("Invalid Id");
        }
        /// <summary>
        /// used to get mom review by reviewId and TraineeId.
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="traineeId"></param>
        /// <returns>
        /// result getMom
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
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
        /// <summary>
        /// used to create mom.
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> CreateMom(MOM mom, int createdBy)
        {
            if (mom is null) throw new ArgumentException(nameof(mom));
            var validation = _repo.Validation.ValidateMOM(mom);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpMomDetails(mom, createdBy);
                _repo.Reviews.CreateMom(mom);
                _repo.Complete();
            }
            return validation;
        }
        /// <summary>
        /// used to Update mom.
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> UpdateMom(MOM mom, int updatedBy)
        {
            if (mom is null) throw new ArgumentException(nameof(mom));
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
    }
}