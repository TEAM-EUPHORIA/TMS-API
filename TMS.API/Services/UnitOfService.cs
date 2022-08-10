using TMS.API.Repositories;

namespace TMS.API.Services
{
    public class UnitOfService : IUnitOfService
    {
        private readonly IUnitOfWork _repo;
        public IAuthService AuthService { get; set; }
        public IUserService UserService { get; set; }
        public IRoleService RoleService { get; set; }
        public IReviewService ReviewService { get; set; }
        public IFeedbackService FeedbackService { get; set; }
        public IDepartmentService DepartmentService { get; set; }
        public ICourseService CourseService { get; set; }
        public IValidation Validation { get; set; }

        public UnitOfService(IUnitOfWork repo, IConfiguration configuration, ILogger<UnitOfService> logger)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            AuthService = new AuthService(repo, configuration, logger);
            UserService = new UserService(repo, logger);
            RoleService = new RoleService(repo, logger);
            ReviewService = new ReviewService(repo, logger);
            FeedbackService = new FeedbackService(repo, logger);
            DepartmentService = new DepartmentService(repo, logger);
            CourseService = new CourseService(repo, logger);
            Validation = repo.Validation;
        }

        public void Complete()
        {
            _repo.Complete();
        }
    }
}