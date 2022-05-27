namespace TMS.API.Services
{
    public partial class UserService
    {
        public Dictionary<string,int> HeadDashboard(AppDbContext dbContext)
        {
            var result = new Dictionary<string,int>();
            prepareHeadDashboard(result,dbContext);
            return result;
        }
        public Dictionary<string,int> CoordinatorDashboard(AppDbContext dbContext)
        {
            var result = new Dictionary<string,int>();
            prepareCoordinatorDashboard(result,dbContext);
            return result;
        }

        public Dictionary<string,int> TraineeDashboard(AppDbContext dbContext)
        {
            var result = new Dictionary<string,int>();
            prepareTraineeDashboard(result,dbContext);
            return result;
        }

        public Dictionary<string,int> TrainerDashboard(AppDbContext dbContext)
        {
            var result = new Dictionary<string,int>();
            prepareTrainerDashboard(result,dbContext);
            return result;
        }

        public Dictionary<string,int> ReviewerDashboard(AppDbContext dbContext)
        {
            var result = new Dictionary<string,int>();
            prepareReviewerDashboard(result,dbContext);
            return result;
        }
    }
}