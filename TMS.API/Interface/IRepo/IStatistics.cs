namespace TMS.API.Services
{
    public interface IStatistics
    {
        int GetAttendanceCount(int courseId, List<int>? topicIds, int userId);
        int GetCompletedReviews(int userId);
        int GetCoordinatorCount();
        int GetCourseCount(int userId);
        int GetCourseCount();
        Dictionary<string, string> GetCourseStats(int userId);
        int GetDepartmentsCount();
        int GetReviewersCount();
        int GetTraineesCount();
        int GetTrainersCount();
        int GetUpComingReviews(int userId);
        int GetUserCount();
        bool IsCourseCompleted(int userId, int courseId);
        int LastUserId();
        Dictionary<string, string> UserDashboardDetails(int userId);
    }
}