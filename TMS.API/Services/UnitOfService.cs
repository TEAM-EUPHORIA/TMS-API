using TMS.API.Repositories;

namespace TMS.API.Services
{
    public class UnitOfService : IUnitOfService
    {
        private readonly UnitOfWork _repo;

        public IAuthService AuthService { get; set; }
        public IUserService UserService { get; set; }
        public IRoleService RoleService { get; set; }
        public IReviewService ReviewService { get; set; }
        public IFeedbackService FeedbackService { get; set; }
        public IDepartmentService DepartmentService { get; set; }
        public ICourseService CourseService { get; set; }
        public IValidation Validation { get; set; }

        public UnitOfService(UnitOfWork repo)
        {
            _repo = repo;
            AuthService = new AuthService(repo);
            UserService = new UserService(repo, repo.Stats);
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