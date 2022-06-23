using TMS.API.Repositories;

namespace TMS.API.Services
{
    public interface IUnitOfService
    {
        AuthService AuthService { get; set; }
        UserService UserService { get; set; }
        RoleService RoleService { get; set; }
        ReviewService ReviewService { get; set; }
        FeedbackService FeedbackService { get; set; }
        DepartmentService DepartmentService { get; set; }
        CourseService CourseService { get; set; }
        Validation Validation { get; set; }

        void Complete();
    }

    public class UnitOfService : IUnitOfService
    {
        private readonly UnitOfWork _repo;

        public AuthService AuthService { get; set; }
        public UserService UserService { get; set; }
        public RoleService RoleService { get; set; }
        public ReviewService ReviewService { get; set; }
        public FeedbackService FeedbackService { get; set; }
        public DepartmentService DepartmentService { get; set; }
        public CourseService CourseService { get; set; }
        public Validation Validation { get; set; }

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