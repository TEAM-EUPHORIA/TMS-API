namespace TMS.API.Services
{
    class Statistics
    {
        private readonly AppDbContext dbContext;

        public Statistics(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
    }
}