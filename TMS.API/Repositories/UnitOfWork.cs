using TMS.API.Services;

namespace TMS.API.Repositories
{
    public class UnitOfWork
    {
        private readonly AppDbContext dbContext;

        public IUserRepository Users { get; set; }
        public IRoleRepository Roles { get; set; }
        public ICourseRepository Courses { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IFeedbackRepository Feedbacks { get; set; }
        public IDepartmentRepository Departments { get; set; }
        public Validation Validation { get; set; }
        public Statistics stats { get; set; }
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
            stats = new Statistics(dbContext);
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }
    }
}