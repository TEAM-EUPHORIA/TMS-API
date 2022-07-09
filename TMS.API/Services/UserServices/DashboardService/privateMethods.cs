using TMS.API.UtilityFunctions;

namespace TMS.API.Services
{
    public partial class UserService
    {
        /// <summary>
        /// used to add data to DashboardResult Dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void AddEntry(string key, string value)
        {
            try
            {
                DashboardResult.Add(key, value);
            }
            catch (ArgumentException ex)
            {
                TMSLogger.ArgumentExceptionInDictionary(ex,_logger,nameof(UserService),nameof(AddEntry));
                throw;
            }
        }
        /// <summary>
        /// used to added user details to DashboardResult Dictionary.
        /// </summary>
        /// <param name="userId"></param>
        private void PrepareUserDetails(int userId)
        {
            var userDashboardDetails = _stats.UserDetails(userId);
            foreach (var item in userDashboardDetails)
            {
                AddEntry(item.Key, item.Value);
            }
        }
        /// <summary>
        /// used to add reviews count based on status to DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void ReviewStats(int userId)
        {
            var upComping = _stats.GetUpComingReviews(userId).ToString();
            var completed = _stats.GetCompletedReviews(userId).ToString();
            AddEntry("Upcomming Review", upComping);
            AddEntry("Completed Review", completed);
        }
        /// <summary>
        /// used to add course related stats to DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void AddCourseStats(int userId)
        {
            var courseStats = _stats.GetCourseStats(userId);
            foreach (var item in courseStats)
            {
                AddEntry(item.Key, item.Value);
            }
        }
        /// <summary>
        /// used to trainees count, trainers count, department count, reviewer count to DashboardResult Dictionary
        /// </summary>
        private void AddTraineeTrainerDepartmentCount()
        {
            AddEntry("traineesCount", _stats.GetTraineesCount().ToString());
            AddEntry("trainersCount", _stats.GetTrainersCount().ToString());
            AddEntry("departmentCount", _stats.GetDepartmentsCount().ToString());
            AddEntry("reviewersCount", _stats.GetReviewersCount().ToString());
        }
        /// <summary>
        /// used to PrepareHeadDashboard by populating DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void PrepareHeadDashboard(int userId)
        {
            PrepareUserDetails(userId);
            AddEntry("coordinatorsCount", _stats.GetCoordinatorCount().ToString());
            AddTraineeTrainerDepartmentCount();
        }
        /// <summary>
        /// used to PrepareCoordinatorDashboard by populating DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void PrepareCoordinatorDashboard(int userId)
        {
            PrepareUserDetails(userId);
            AddEntry("courseCount", _stats.GetCourseCount().ToString());
            AddTraineeTrainerDepartmentCount();
        }
        /// <summary>
        /// used to PrepareTraineeDashboard by populating DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void PrepareTraineeDashboard(int userId)
        {
            PrepareUserDetails(userId);
            AddCourseStats(userId);
            ReviewStats(userId);
        }
        /// <summary>
        /// used to PrepareTrainerDashboard by populating DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void PrepareTrainerDashboard(int userId)
        {
            PrepareUserDetails(userId);
            AddCourseStats(userId);
        }
        /// <summary>
        /// used to PrepareReviewerDashboard by populating DashboardResult Dictionary
        /// </summary>
        /// <param name="userId"></param>
        private void PrepareReviewerDashboard(int userId)
        {
            PrepareUserDetails(userId);
            ReviewStats(userId);
        }
    }
}