using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public partial class Validation
    {
        public bool AttendanceExists(int courseId,int topicId,int ownerId)
        {
            return dbContext.Attendances.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == ownerId);
        } 
        public bool AssignmentExists(int courseId,int topicId,int ownerId)
        {
            return dbContext.Assignments.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == ownerId);
        } 
        public bool CourseUserExists(int courseId,int userId,int roleId)
        {
            return dbContext.CourseUsers.Any(cu=> cu.CourseId == courseId && cu.UserId == userId && cu.RoleId == roleId);
        } 
        public bool CourseExists(int courseId)
        {
            return dbContext.Courses.Any(c=> c.Id == courseId);
        } 
        public bool IsCourseNameAvailable(int courseId,int departmentId,string name)
        {
            return dbContext.Courses.Any(c=>c.Id != courseId && c.DepartmentId == departmentId && c.Name==name);
        } 
         public bool IsTopicNameAvailabe(int topicId,int courseId,string name)
        {
            return dbContext.Topics.Any(c=>c.TopicId!=topicId && c.CourseId == courseId && c.Name==name);
        } 

        public bool DepartmentExists(int? departmentId)
        {
            return dbContext.Departments.Any(d=>d.Id == departmentId);
        } 
        public bool MOMExists(int reviewId,int traineeId)
        {
            return dbContext.MOMs.Any(m=>m.ReviewId == reviewId && m.TraineeId == traineeId);
        } 
        public bool ReviewExists(int reviewId )
        {
            return dbContext.Reviews.Any(r=>r.Id==reviewId);
        } 
        public bool ReviewerAvailabilityExists(int id,int reviewerId,DateTime reviewDate,DateTime reviewTime,int statusId)
        {
            return dbContext.Reviews.Any(r=>r.Id!=id && r.ReviewerId==reviewerId && r.ReviewDate==reviewDate && r.ReviewTime==reviewTime && statusId==1);
        } 
         public bool TraineeAvailabilityExists(int id,int traineeId,DateTime reviewDate,DateTime reviewTime,int statusId)
        {
            return dbContext.Reviews.Any(r=>r.Id!=id && r.TraineeId==traineeId && r.ReviewDate==reviewDate && r.ReviewTime==reviewTime && statusId==1);
        } 
         public bool MOMReviewExists(int reviewId ,int traineeId)
        {
            return dbContext.Reviews.Any(r=>r.Id==reviewId && r.TraineeId==traineeId && r.StatusId==2);
        } 
        public bool ReviewStatusExists(int statusId)
        {
            return dbContext.ReviewStatuses.Any(r=>r.Id == statusId);
        } 
        public bool RoleExists(int roleId)
        {
            return dbContext.Roles.Any(r=>r.Id==roleId);
        } 
        public bool TopicExists(int topicId,int courseId)
        {
            return dbContext.Topics.Any(t=>t.TopicId==topicId && t.CourseId == courseId);
        } 
        public bool TopicExists(int topicId)
        {
            return dbContext.Topics.Any(t=>t.TopicId==topicId);
        } 
        public bool TopicValidationExists(int courseId,string name)
        {
            return dbContext.Topics.Any(t=>t.CourseId==courseId && t.Name==name);
        } 
        public bool TraineeFeedbackExists(int courseId,int traineeId,int trainerId)
        {
            return dbContext.TraineeFeedbacks.Any(tf=>tf.CourseId==courseId && tf.TraineeId == traineeId && tf.TrainerId == trainerId);
        } 
        public bool CourseFeedbackExists(int courseId,int traineeId)
        {
            return dbContext.CourseFeedbacks.Any(cf=>cf.CourseId==courseId && cf.TraineeId == traineeId);
        } 
        public bool UserExists(int userId)
        {
            return dbContext.Users.Any(u=>u.Id==userId);
        } 
        public bool UserExists(int userId,int roleId)
        {
            return dbContext.Users.Any(u=>u.Id==userId && u.RoleId == roleId);
        } 
        public bool UserExists( LoginModel user)
        {
            return dbContext.Users.Any(u=>u.Email==user.Email && u.Password==HashPassword.Sha256(user.Password));
        } 
    }
}