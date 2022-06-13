using Microsoft.EntityFrameworkCore;

namespace TMS.API.Services
{
    public class Statistics
    {
        private readonly AppDbContext dbContext;

        public Statistics(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int lastUserId()
        {
            return dbContext.Users.OrderBy(u=>u.Id).Last().Id;
        }
        public int GetCoordinatorCount()
        {
            return dbContext.Users.Select(u=>u.RoleId == 2).Count();
        }
        public int GetDepartmentsCount()
        {
            return dbContext.Departments.Count();
        }
        public int GetTraineesCount()
        {
            return dbContext.Users.Select(u=>u.RoleId == 4).Count();
        }
        public int GetTrainersCount()
        {
            return dbContext.Users.Where(u=>u.RoleId == 3).Count();
        }
        public int GetCourseCount(int userId)
        {
            return dbContext.CourseUsers.Select(cu=>cu.UserId == userId).Count();
        }
        public int GetCompletedReviews(int userId)
        {
            return dbContext.Reviews.Where(r=>r.StatusId == 2 && r.ReviewerId == userId).Count();
        }
        public int GetUpComingReviews(int userId)
        {
            return dbContext.Reviews.Where(r=>r.TraineeId == userId && r.StatusId == 1 && r.ReviewDate.Day >= DateTime.Now.Day).Count();
        }
        public int GetCanceledReviews()
        {
            return dbContext.Reviews.Where(r=>r.StatusId == 3).Count();
        }
        public int GetAttendanceCount(int courseId,List<int>? topicIds,int userId)
        {
            bool present = false;
            int result = 0;
            if(topicIds != null)
            {
                foreach (var topicId in topicIds)
                {
                    present = dbContext.Attendances.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == userId);
                    if(present) result++;
                }
            }
            return result;
        }
        public Dictionary<string,string> GetCourseStats(int userId)
        {
            var courseIds = dbContext.CourseUsers.Where(cu=>cu.UserId == userId).Select(cu=>cu.CourseId).ToList();
            bool isCompleted = false;
            int courseCount = courseIds.Count();
            int completedCourseCount = 0;
            foreach (var courseId in courseIds)
            {
                isCompleted = IsCourseCompleted(userId,courseId);
                if(isCompleted) 
                    completedCourseCount++;
            }
            int inProgressCourseCount = courseCount - completedCourseCount;
            var result = new Dictionary<string,string>();
            result.Add(nameof(courseCount),courseCount.ToString());
            result.Add(nameof(completedCourseCount),completedCourseCount.ToString());
            result.Add(nameof(inProgressCourseCount),inProgressCourseCount.ToString());
            return result;
        }
        public bool IsCourseCompleted(int userId,int courseId)
        {
            var topicIds = dbContext.Topics.Where(t=>t.CourseId == courseId).Select(t=>t.TopicId).ToList();
            int topicsCount = topicIds.Count();
            int attendanceCount = GetAttendanceCount(courseId,topicIds,userId);
            return topicsCount == attendanceCount ? true: false;
        }
        public Dictionary<string,string> userDetails(int userId)
        {
            var user = dbContext.Users.Where(u=>u.Id == userId).Include(u=>u.Role).FirstOrDefault();
            var result = new Dictionary<string,string>();
            if(user!=null)
            {
                result.Add("Name",user.FullName);
                result.Add("Role",user.Role.Name);
                result.Add("Image",user.Base64 + user.Image);
            }
            return result;
        }
    }
}