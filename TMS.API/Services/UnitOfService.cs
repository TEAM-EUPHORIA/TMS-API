using TMS.API.Repositories;

namespace TMS.API.Services
{
    public class UnitOfService
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
            UserService = new UserService(_repo);
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