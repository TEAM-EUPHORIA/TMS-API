namespace TMS.API.Services
{
    public partial class UserService
    {   
       private Dictionary<string,string> prepareHeadDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            result.Add("coordinatorsCount", _stats.GetCoordinatorCount().ToString());
            AddTraineeTrainerDepartmentCount(result);
            return result;
        }

        private void AddTraineeTrainerDepartmentCount(Dictionary<string, string> result)
        {
            result.Add("traineesCount", _stats.GetTraineesCount().ToString());
            result.Add("trainersCount", _stats.GetTrainersCount().ToString());
            result.Add("departmentCount", _stats.GetDepartmentsCount().ToString());
        }

        private Dictionary<string,string>? prepareCoordinatorDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            result.Add("courseCount",_stats.GetCourseCount(userId).ToString());
            AddTraineeTrainerDepartmentCount(result);
            return result;
        }
        private Dictionary<string,string> prepareTraineeDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            AddCourseStats(userId, result);
            AddUpcomingReviewCount(userId, result);
            return result;
        }

        private void AddUpcomingReviewCount(int userId, Dictionary<string, string> result)
        {
            result.Add("upCompingReviews", _stats.GetUpComingReviews(userId).ToString());
        }

        private void AddCourseStats(int userId, Dictionary<string, string> result)
        {
            var courseStats = _stats.GetCourseStats(userId);
            foreach (var item in courseStats)
            {
                result.Add(item.Key, item.Value);
            }
        }

        private Dictionary<string,string> prepareTrainerDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            AddCourseStats(userId,result);
            return result;
        }
        private Dictionary<string,string> prepareReviewerDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            AddUpcomingReviewCount(userId, result);
            return result;
        }
    }
}