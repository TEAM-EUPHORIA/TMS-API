namespace TMS.API.Services
{
    public partial class UserService
    {
        public Dictionary<string,int> HeadDashboard()
        {
            var result = new Dictionary<string,int>();
            // prepareHeadDashboard(result);
            return result;
        }
        public Dictionary<string,int> CoordinatorDashboard()
        {
            var result = new Dictionary<string,int>();
            // prepareCoordinatorDashboard(result);
            return result;
        }

        public Dictionary<string,int> TraineeDashboard()
        {
            var result = new Dictionary<string,int>();
            // prepareTraineeDashboard(result);
            return result;
        }

        public Dictionary<string,int> TrainerDashboard()
        {
            var result = new Dictionary<string,int>();
            // prepareTrainerDashboard(result);
            return result;
        }

        public Dictionary<string,int> ReviewerDashboard()
        {
            var result = new Dictionary<string,int>();
            // prepareReviewerDashboard(result);
            return result;
        }
    }
}