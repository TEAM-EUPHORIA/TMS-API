using TMS.API.Repositories;

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
            result.Add("reviewersCount", _stats.GetReviewersCount().ToString());
        }

        private Dictionary<string,string>? prepareCoordinatorDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            result.Add("courseCount",_stats.GetCourseCount().ToString());
            AddTraineeTrainerDepartmentCount(result);
            return result;
        }
        private Dictionary<string,string> prepareTraineeDashboard(int userId)
        {
            var result = _stats.userDetails(userId);
            AddCourseStats(userId, result);
            ReviewStats(userId, result);
            return result;
        }

        private void ReviewStats(int userId, Dictionary<string, string> result)
        {
             var upComping = _stats.GetUpComingReviews(userId).ToString();
            var completed = _stats.GetUpComingReviews(userId).ToString();
            var canceled = _stats.GetUpComingReviews(userId).ToString();
            result.Add("Upcomming Review",upComping);
            result.Add("Canceled Review",canceled);
            result.Add("Completed Review",completed);
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
            ReviewStats(userId, result);
            return result;
        }
    }
}