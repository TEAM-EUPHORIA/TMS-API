using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public static partial class Validation
    {
        public static bool AttendanceExists(AppDbContext dbContext, int courseId,int topicId,int ownerId)
        {
            return dbContext.Attendances.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == ownerId);
        } 
        public static bool AssignmentExists(AppDbContext dbContext, int courseId,int topicId,int ownerId)
        {
            return dbContext.Assignments.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == ownerId);
        } 
        public static bool CourseUserExists(AppDbContext dbContext, int courseId,int userId,int roleId)
        {
            return dbContext.CourseUsers.Any(cu=> cu.CourseId == courseId && cu.UserId == userId && cu.RoleId == roleId);
        } 
        public static bool CourseExists(AppDbContext dbContext, int courseId)
        {
            return dbContext.Courses.Any(c=> c.Id == courseId);
        } 
        public static bool DepartmentExists(AppDbContext dbContext, int departmentId)
        {
            return dbContext.Departments.Any(d=>d.Id == departmentId);
        } 
        public static bool MOMExists(AppDbContext dbContext, int reviewId,int traineeId)
        {
            return dbContext.MOMs.Any(m=>m.ReviewId == reviewId && m.TraineeId == traineeId);
        } 
        public static bool ReviewExists(AppDbContext dbContext, int reviewId)
        {
            return dbContext.Reviews.Any(r=>r.Id==reviewId);
        } 
        public static bool ReviewStatusExists(AppDbContext dbContext,int statusId)
        {
            return dbContext.ReviewStatuses.Any(r=>r.Id == statusId);
        } 
        public static bool RoleExists(AppDbContext dbContext, int roleId)
        {
            return dbContext.Roles.Any(r=>r.Id==roleId);
        } 
        public static bool TopicExists(AppDbContext dbContext, int topicId)
        {
            return dbContext.Topics.Any(t=>t.TopicId==topicId);
        } 
        public static bool TraineeFeedbackExists(AppDbContext dbContext, int courseId,int traineeId, int trainerId)
        {
            return dbContext.TraineeFeedbacks.Any(tf=>tf.CourseId==courseId && tf.TraineeId == traineeId && tf.TrainerId == trainerId);
        } 
        public static bool CourseFeedbackExists(AppDbContext dbContext, int courseId,int traineeId)
        {
            return dbContext.CourseFeedbacks.Any(cf=>cf.CourseId==courseId && cf.TraineeId == traineeId);
        } 
        public static bool UserExists(AppDbContext dbContext, int userId)
        {
            return dbContext.Users.Any(u=>u.Id==userId);
        } 
        public static bool UserExists(AppDbContext dbContext, int userId,int roleId)
        {
            return dbContext.Users.Any(u=>u.Id==userId && u.RoleId == roleId);
        } 
        public static bool UserExists(AppDbContext dbContext, LoginModel user)
        {
            return dbContext.Users.Any(u=>u.Email==user.Email && u.Password==HashPassword.Sha256(user.Password));
        } 
    }
}