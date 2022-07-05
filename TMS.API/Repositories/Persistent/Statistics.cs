using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TMS.API.Services
{

    public class Statistics : IStatistics
    {
        private readonly AppDbContext dbContext;

        public Statistics(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int GetUserCount()
        {
            // return dbContext.Users.Count();
            return 1;
        }
        public int lastUserId()
        {
            return dbContext.Users.OrderBy(u => u.Id).Last().Id;
        }
        public int GetCoordinatorCount()
        {
            return dbContext.Users.Where(u=>u.RoleId == 2 && u.isDisabled == false).Count();
        }
        public int GetDepartmentsCount()
        {
            return dbContext.Departments.Where(d=>d.isDisabled == false).Count();
        }
        public int GetTraineesCount()
        {
            return dbContext.Users.Where(u=>u.RoleId == 4 && u.isDisabled == false).Count();
        }
        public int GetTrainersCount()
        {
            return dbContext.Users.Where(u=>u.RoleId == 3 && u.isDisabled == false).Count();
        }
        public int GetReviewersCount(){
            return dbContext.Users.Where(u=>u.RoleId == 5 && u.isDisabled == false).Count();
        }
        public int GetCourseCount(int userId)
        {
            return dbContext.CourseUsers.Where(cu=>cu.UserId == userId && cu.Course.isDisabled == false).Count();
        }
        public int GetCourseCount()
        {
            return dbContext.Courses.Where(c=>c.isDisabled == false).Count();
        }
        public int GetCompletedReviews(int userId)
        {
            return dbContext.Reviews.Where(r=>r.StatusId == 2 && r.ReviewerId == userId && r.Reviewer.isDisabled == false).Count();
        }
        public int GetUpComingReviews(int userId)
        {
            return dbContext.Reviews.Where(r=>r.TraineeId == userId && r.StatusId == 1 && r.ReviewDate.Day >= DateTime.Now.Day && (r.Reviewer.isDisabled == false && r.Trainee.isDisabled == false) ).Count();
        }
        public int GetCanceledReviews()
        {
            return dbContext.Reviews.Where(r=>r.StatusId == 3 && (r.Trainee.isDisabled == false && r.Reviewer.isDisabled == false) ).Count();
        }
        public int GetAttendanceCount(int courseId, List<int>? topicIds, int userId)
        {
            bool present = false;
            int result = 0;
            if (topicIds != null)
            {
                foreach (var topicId in topicIds)
                {
                    present = dbContext.Attendances.Any(a => a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == userId);
                    if (present) result++;
                }
            }
            return result;
        }
        public Dictionary<string, string> GetCourseStats(int userId)
        {
            var courseIds = dbContext.CourseUsers.Where(cu=>cu.UserId == userId && cu.Course.isDisabled == false).Select(cu=>cu.CourseId).ToList();
            bool isCompleted = false;
            int courseCount = courseIds.Count();
            int completedCourseCount = 0;
            foreach (var courseId in courseIds)
            {
                isCompleted = IsCourseCompleted(userId, courseId);
                if (isCompleted)
                    completedCourseCount++;
            }
            int inProgressCourseCount = courseCount - completedCourseCount;
            var result = new Dictionary<string, string>();
            result.Add(nameof(courseCount), courseCount.ToString());
            result.Add(nameof(completedCourseCount), completedCourseCount.ToString());
            result.Add(nameof(inProgressCourseCount), inProgressCourseCount.ToString());
            return result;
        }
        public bool IsCourseCompleted(int userId, int courseId)
        {
            var topicIds = dbContext.Topics.Where(t => t.CourseId == courseId).Select(t => t.TopicId).ToList();
            int topicsCount = topicIds.Count();
            int attendanceCount = GetAttendanceCount(courseId, topicIds, userId);
            return topicsCount == attendanceCount ? true : false;
        }
        public Dictionary<string, string> userDetails(int userId)
        {
            var user = dbContext.Users.Where(u => u.Id == userId).Include(u => u.Role).FirstOrDefault();
            var result = new Dictionary<string, string>();
            if (user != null)
            {
                result.Add("Name", user.FullName);
                result.Add("Role", user.Role.Name);
                result.Add("Base64", user.Base64);
                result.Add("Image", Convert.ToBase64String(user.Image));
            }
            return result;
        }
    }
}