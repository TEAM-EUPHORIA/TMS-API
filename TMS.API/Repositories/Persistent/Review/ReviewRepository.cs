namespace TMS.API.Repositories
{
    public partial class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext dbContext;
        public ReviewRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}