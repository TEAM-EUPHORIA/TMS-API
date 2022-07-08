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

        public UnitOfService(IUnitOfWork repo,IConfiguration configuration)
        {
            _repo = repo;
            AuthService = new AuthService(repo,configuration);
            UserService = new UserService(repo);
            RoleService = new RoleService(repo);
            ReviewService = new ReviewService(repo);
            FeedbackService = new FeedbackService(repo);
            DepartmentService = new DepartmentService(repo);
            CourseService = new CourseService(repo);
            Validation = repo.Validation;
        }

        public void Complete()
        {
            _repo.Complete();
        }
    }
}