using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API
{
    public partial class Validation
    {
        /// <summary>
        /// check if Attendance Exists
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        /// <returns>
        /// true if AttendanceExists else false
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool AttendanceExists(int courseId, int topicId, int ownerId)
        {
            return dbContext.Attendances.Any(a =>
                    a.CourseId == courseId &&
                    a.TopicId == topicId &&
                    a.OwnerId == ownerId);
        }
        /// <summary>
        /// check if Assignment Exists
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        /// <returns>
        /// true if AssignmentExists else false
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool AssignmentExists(int courseId, int topicId, int ownerId)
        {
            return dbContext.Assignments.Any(a =>
                    a.CourseId == courseId &&
                    a.TopicId == topicId &&
                    a.OwnerId == ownerId);
        }
        /// <summary>
        /// check if course user exists
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns>
        /// true if course user exists else false
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool CourseUserExists(int courseId, int userId, int roleId)
        {
            return dbContext.CourseUsers.Any(cu =>
                    cu.CourseId == courseId &&
                    cu.UserId == userId &&
                    cu.RoleId == roleId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool CourseExists(int courseId)
        {
            return dbContext.Courses.Any(c =>
                    c.Id == courseId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="departmentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool IsCourseNameAvailable(int courseId, int departmentId, string name)
        {
            return dbContext.Courses.Any(c =>
                    c.Id != courseId &&
                    c.DepartmentId == departmentId &&
                    c.Name == name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="courseId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool IsTopicNameAvailabe(int topicId, int courseId, string name)
        {
            return dbContext.Topics.Any(c =>
                    c.TopicId != topicId &&
                    c.CourseId == courseId &&
                    c.Name == name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool DepartmentExists(int departmentId)
        {
            return dbContext.Departments.Any(d =>
                    d.Id == departmentId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool MOMExists(int reviewId, int traineeId)
        {
            return dbContext.MOMs.Any(m =>
                    m.ReviewId == reviewId &&
                    m.TraineeId == traineeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool ReviewExists(int reviewId)
        {
            return dbContext.Reviews.Any(r =>
                    r.Id == reviewId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reviewerId"></param>
        /// <param name="reviewDate"></param>
        /// <param name="reviewTime"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool ReviewerAvailabilityExists(
            int id, 
            int reviewerId, 
            DateTime reviewDate,
            DateTime reviewTime, 
            int statusId)
        {
            return dbContext.Reviews.Any(r =>
                    r.Id != id &&
                    r.ReviewerId == reviewerId &&
                    r.ReviewDate == reviewDate &&
                    r.ReviewTime == reviewTime &&
                    statusId == 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="traineeId"></param>
        /// <param name="reviewDate"></param>
        /// <param name="reviewTime"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool TraineeAvailabilityExists(
            int id, 
            int traineeId, 
            DateTime reviewDate, 
            DateTime reviewTime, 
            int statusId)
        {
            return dbContext.Reviews.Any(r =>
                    r.Id != id &&
                    r.TraineeId == traineeId &&
                    r.ReviewDate == reviewDate &&
                    r.ReviewTime == reviewTime &&
                    statusId == 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool MOMReviewExists(int reviewId, int traineeId)
        {
            return dbContext.Reviews.Any(r =>
                    r.Id == reviewId &&
                    r.TraineeId == traineeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool ReviewStatusExists(int statusId)
        {
            return dbContext.ReviewStatuses.Any(r =>
                    r.Id == statusId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool RoleExists(int roleId)
        {
            return dbContext.Roles.Any(r =>
                    r.Id == roleId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool TopicExists(int topicId, int courseId)
        {
            return dbContext.Topics.Any(t =>
                    t.TopicId == topicId &&
                    t.CourseId == courseId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool TopicExists(int topicId)
        {
            return dbContext.Topics.Any(t =>
                    t.TopicId == topicId);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool TopicValidationExists(int courseId, string name)
        {
            return dbContext.Topics.Any(t =>
                    t.CourseId == courseId &&
                    t.Name == name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool TraineeFeedbackExists(int courseId, int traineeId, int trainerId)
        {
            return dbContext.TraineeFeedbacks.Any(tf =>
                    tf.CourseId == courseId &&
                    tf.TraineeId == traineeId &&
                    tf.TrainerId == trainerId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool CourseFeedbackExists(int courseId, int traineeId)
        {
            return dbContext.CourseFeedbacks.Any(cf =>
                    cf.CourseId == courseId &&
                    cf.TraineeId == traineeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool UserExists(int userId)
        {
            return dbContext.Users.Any(u =>
                    u.Id == userId && u.isDisabled == false);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool UserExists(int userId, int roleId)
        {
            return dbContext.Users.Any(u =>
                    u.Id == userId &&
                    u.RoleId == roleId &&
                    u.isDisabled == false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public bool UserExists(LoginModel user)
        {
            return dbContext.Users.Any(u =>
                    u.Email == user.Email
                    && u.Password == HashPassword.Sha256(user.Password)
                    && u.isDisabled == false);
        }
    }
}