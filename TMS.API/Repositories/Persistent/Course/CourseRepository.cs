namespace TMS.API.Repositories
{
    public partial class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext dbContext;
        public CourseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}