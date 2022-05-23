using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public static partial class Validation
    {
        public static bool AssignmentStatusExists(AppDbContext dbContext, int AssignmentStatusId)
        {
            return dbContext.AssignmentStatuses.Any(e=>e.Id==AssignmentStatusId);
        } 
        public static bool AttendanceStatusExists(AppDbContext dbContext, int AttendanceStatusId)
        {
            return dbContext.AttendanceStatuses.Any(e=>e.Id==AttendanceStatusId);
        } 
        public static bool CourseStatusExists(AppDbContext dbContext, int CourseStatusId)
        {
            return dbContext.CourseStatuses.Any(e=>e.Id==CourseStatusId);
        } 
        public static bool TraineeFeedbackStatusExists(AppDbContext dbContext, int TraineeFeedbackStatusId)
        {
            return dbContext.TraineeFeedbackStatuses.Any(e=>e.Id==TraineeFeedbackStatusId);
        } 
        public static bool ReviewStatusExists(AppDbContext dbContext, int ReviewStatusId)
        {
            return dbContext.ReviewStatuses.Any(e=>e.Id==ReviewStatusId);
        } 
        public static bool MOMStatusExists(AppDbContext dbContext, int MOMStatusId)
        {
            return dbContext.MOMStatuses.Any(e=>e.Id==MOMStatusId);
        } 
        public static bool AssignmentExists(AppDbContext dbContext, int assignmentId)
        {
            return dbContext.Assignments.Any(e=>e.Id==assignmentId);
        } 
        public static bool AttendanceExists(AppDbContext dbContext, int attendanceId)
        {
            return dbContext.Attendances.Any(e=>e.Id==attendanceId);
        } 
        public static bool CourseExists(AppDbContext dbContext, int courseId)
        {
            return dbContext.Courses.Any(e=>e.Id==courseId);
        } 
        public static bool DepartmentExists(AppDbContext dbContext, int departmentId)
        {
            return dbContext.Departments.Any(e=>e.Id==departmentId);
        } 
        public static bool MOMExists(AppDbContext dbContext, int momId)
        {
            return dbContext.MOMs.Any(e=>e.Id==momId);
        } 
        public static bool ReviewExists(AppDbContext dbContext, int reviewId)
        {
            return dbContext.Reviews.Any(e=>e.Id==reviewId);
        } 
        public static bool RoleExists(AppDbContext dbContext, int roleId)
        {
            return dbContext.Roles.Any(e=>e.Id==roleId);
        } 
        public static bool TopicExists(AppDbContext dbContext, int topicId)
        {
            return dbContext.Topics.Any(e=>e.Id==topicId);
        } 
        public static bool TraineeFeedbackExists(AppDbContext dbContext, int traineeFeedbackId)
        {
            return dbContext.TraineeFeedbacks.Any(e=>e.Id==traineeFeedbackId);
        } 
        public static bool CourseFeedbackExists(AppDbContext dbContext, int courseFeedbackId)
        {
            return dbContext.CourseFeedbacks.Any(e=>e.Id==courseFeedbackId);
        } 
        public static bool UserExists(AppDbContext dbContext, int userId)
        {
            return dbContext.Users.Any(e=>e.Id==userId);
        } 
        public static bool UserExists(AppDbContext dbContext, LoginModel user)
        {
            return dbContext.Users.Any(e=>e.Email==user.Email && e.Password == HashPassword.Sha256(user.Password));
        } 
        public static bool FeedbackExists(AppDbContext dbContext, int courseId, int ownerId)
        {
            return dbContext.CourseFeedbacks.Any(e=>e.CourseId==courseId && e.OwnerId==ownerId);
        } 

    }
}