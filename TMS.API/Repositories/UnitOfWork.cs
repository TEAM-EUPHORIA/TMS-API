using TMS.API.Services;

namespace TMS.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public IUserRepository Users { get; set; }
        public IRoleRepository Roles { get; set; }
        public ICourseRepository Courses { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IFeedbackRepository Feedbacks { get; set; }
        public IDepartmentRepository Departments { get; set; }
        public IValidation Validation { get; set; }
        public Statistics Stats { get; set; }
        public object DepartmentService { get; set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            Users = new UserRepository(dbContext);
            Roles = new RoleRepository(dbContext);
            Courses = new CourseRepository(dbContext);
            Reviews = new ReviewRepository(dbContext);
            Feedbacks = new FeedbackRepository(dbContext);
            Departments = new DepartmentRepository(dbContext);
            Validation = new Validation(dbContext);
            Stats = new Statistics(dbContext);
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }
    }
}