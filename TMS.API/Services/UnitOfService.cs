using TMS.API.Repositories;

namespace TMS.API.Services
{
    public interface IUnitOfService
    {
        IAuthService AuthService { get; set; }
        IUserService UserService { get; set; }
        IRoleService RoleService { get; set; }
        IReviewService ReviewService { get; set; }
        IFeedbackService FeedbackService { get; set; }
        IDepartmentService DepartmentService { get; set; }
        ICourseService CourseService { get; set; }
        IValidation Validation { get; set; }

        void Complete();
    }

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
            AuthService = new AuthService(_repo);
            UserService = new UserService(_repo, _repo.stats);
            RoleService = new RoleService(_repo);
            ReviewService = new ReviewService(_repo);
            FeedbackService = new FeedbackService(_repo);
            DepartmentService = new DepartmentService(_repo);
            CourseService = new CourseService(_repo);
            Validation = _repo.Validation;
        }

        public void Complete()
        {
            _repo.Complete();
        }
    }
}