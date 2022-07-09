namespace TMS.API.Repositories
{
    public partial class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext dbContext;
        public FeedbackRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}