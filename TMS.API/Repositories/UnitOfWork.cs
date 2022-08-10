using Microsoft.EntityFrameworkCore;
using TMS.API.Services;
using TMS.API.UtilityFunctions;

namespace TMS.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger _logger;

        public IUserRepository Users { get; set; }
        public IRoleRepository Roles { get; set; }
        public ICourseRepository Courses { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IFeedbackRepository Feedbacks { get; set; }
        public IDepartmentRepository Departments { get; set; }
        public IValidation Validation { get; set; }
        public IStatistics Stats { get; set; }


        public UnitOfWork(AppDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
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
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TMSLogger.DbException(ex, _logger, nameof(Complete));
                throw;
            }
        }
    }
}