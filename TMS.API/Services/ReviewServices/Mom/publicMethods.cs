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
            _repo = repo;
            _logger = logger;
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
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

        /// <summary>
        /// used to get mom review by reviewId and TraineeId.
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="traineeId"></param>
        /// <returns>
        /// result getMom
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

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

        /// <summary>
        /// used to create mom.
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

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

        /// <summary>
        /// used to Update mom.
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
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
    }
}