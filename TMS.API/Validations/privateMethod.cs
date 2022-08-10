using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API
{
    public partial class Validation
    {
        /// <summary>
        /// used to Add IsValid to Dictionary
        /// </summary>
        private void CheckIsValid()
        {
            if (result.Count == 0 ||
               (result.ContainsKey("Exists") && !result.ContainsKey("IsValid")))
            {
                AddEntery("IsValid", "true");
            }
        }
        /// <summary>
        /// sets courseUsertExists to true if the condition holds true
        /// </summary>
        /// <param name="user"></param>
        private void CheckCourseUserExists(CourseUsers user)
        {
            if (result.Count == 0 &&
            courseExists && userExists)
            {
                courseUsertExists = CourseUserExists(
                    user.CourseId,
                    user.UserId,
                    user.RoleId);
            }
        }
        /// <summary>
        /// sets usertExists to true if the user actually exists
        /// </summary>
        /// <param name="user"></param>
        private void CheckUserExists(LoginModel user)
        {
            if (result.Count == 0)
            {
                userExists = UserExists(user);
            }
        }
        /// <summary>
        /// sets usertExists to true if the user actually exists
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        private void CheckUserExists(int userId, int roleId)
        {
            userExists = UserExists(userId, roleId);
        }
        /// <summary>
        /// sets usertExists to true if the user actually exists
        /// </summary>
        /// <param name="userId"></param>
        private void CheckUserExists(int userId)
        {
            userExists = UserExists(userId);
        }
        /// <summary>
        /// sets courseExists to true if the course actually exists
        /// </summary>
        /// <param name="courseId"></param>
        private void CheckCourseExists(int courseId)
        {
            courseExists = CourseExists(courseId);
        }
        /// <summary>
        /// sets assignmentExists to true if the assignment actually exists
        /// </summary>
        /// <param name="assignment"></param>
        private void CheckAssignmentExists(Assignment assignment)
        {
            if (result.Count == 0 &&
                courseExists &&
                topicExists &&
                userExists)
            {
                assignmentExists = AssignmentExists(
                    assignment.CourseId,
                    assignment.TopicId,
                    assignment.OwnerId);
            }
        }
        /// <summary>
        /// sets topicExists to true if the topic actually exists
        /// </summary>
        /// <param name="topicId"></param>
        private void CheckTopicExists(int topicId)
        {
            topicExists = TopicExists(topicId);
        }
        /// <summary>
        /// sets topicExists to true if the topic actually exists
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="courseId"></param>
        private void CheckTopicExists(int topicId, int courseId)
        {
            topicExists = TopicExists(topicId, courseId);
        }
        /// <summary>
        /// sets attendanceExists to true if the attendance actually exists
        /// </summary>
        /// <param name="attendance"></param>
        private void CheckAttendanceExists(Attendance attendance)
        {
            if (courseExists &&
                topicExists &&
                userExists)
            {
                attendanceExists = AttendanceExists(
                    attendance.CourseId,
                    attendance.TopicId,
                    attendance.OwnerId);
            }
        }
        /// <summary>
        /// checks course exists if course id is not zero
        /// and adds course not found if courseExists is false
        /// </summary>
        /// <param name="course"></param>
        private void CheckCourseIfIdIsNotZero(Course course)
        {
            if (course.Id != 0)
            {
                CheckCourseExists(course.Id);
                if (!courseExists)
                    CourseNotFound();
            }
        }
        /// <summary>
        /// checks department exists if department id is not zero
        /// and adds department not found if departmentExists is false
        /// </summary>
        /// <param name="departmentId"></param>
        private void CheckDepartmentExistsIfIdIsNotZero(int departmentId)
        {
            if (departmentId != 0)
            {
                departmentExists = DepartmentExists(departmentId);
                if (!departmentExists)
                    DepartmentNotFound();
            }
        }
        /// <summary>
        /// checks if course name is available
        /// </summary>
        /// <param name="course"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckForCourseNameAvailablity(Course course)
        {
            if (course is null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            if (userExists && departmentExists)
                isCourseNameAvailable = IsCourseNameAvailable(
                    course.Id,
                    course.DepartmentId,
                    course.Name!);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckForUserMailAvailablity(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (departmentExists && roleExists)
                isUserMailAvailable = IsUserMailAvailable(
                    user.Id,
                    user.Email!);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckForUserMailAvailablity(UpdateUserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (departmentExists && roleExists)
                isUserMailAvailable = IsUserMailAvailable(
                    user.Id,
                    user.Email!);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        private void CheckForDepartmentNameAvailablity(Department department)
        {
            if (department is null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            isDepartMentNameAvailable = IsDepartmentNameAvailable(
                department.Id,
                department.Name!);
        }
        /// <summary>
        /// sets departmentExists to true if the department actually exists 
        /// </summary>
        /// <param name="departmentId"></param>
        private void CheckDepartmentExists(int departmentId)
        {
            departmentExists = DepartmentExists(departmentId);
        }
        /// <summary>
        /// sets courseFeedbackExists to true if the feedback actually exists 
        /// </summary>
        /// <param name="feedback"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckCourseFeedbackExists(CourseFeedback feedback)
        {
            if (feedback is null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            if (courseExists && userExists)
                courseFeedbackExists = CourseFeedbackExists(
                    feedback.CourseId,
                    feedback.TraineeId);
        }
        /// <summary>
        /// check the rating and adds validation msg
        /// </summary>
        /// <param name="rating"></param>
        private void CheckIfRatingIsValid(float rating)
        {
            if (rating is <= 0 or > 5)
                AddEntery("rating", "rating must be between 0 to 5");
        }
        /// <summary>
        /// sets mom exists to true if the condition holds true
        /// </summary>
        /// <param name="mom"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckMomExists(MOM mom)
        {
            if (mom is null)
            {
                throw new ArgumentNullException(nameof(mom));
            }

            if (userExists && reviewExists)
                momExists = MOMExists(mom.ReviewId, mom.TraineeId);
        }
        /// <summary>
        /// sets reviewExists to true if the review was scheduled
        /// </summary>
        /// <param name="mom"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckMomReviewExists(MOM mom)
        {
            if (mom is null)
            {
                throw new ArgumentNullException(nameof(mom));
            }

            reviewExists = MOMReviewExists(mom.ReviewId, mom.TraineeId);
        }
        /// <summary>
        /// sets traineeExists to true 
        /// </summary>
        /// <param name="traineeId"></param>
        
        private void CheckTraineeExists(int traineeId)
        {
            traineeExists = UserExists(traineeId, 4);
        }
        /// <summary>
        /// checks trainee availablity for review if trainee is not available adds msg
        /// </summary>
        /// <param name="review"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckTraineeAvailablity(Review review)
        {
            if (review is null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            if (userExists && traineeExists && reviewStatusExists)
                traineeAvailabilityExists =
                TraineeAvailabilityExists(review.Id,
                    review.TraineeId,
                    review.ReviewDate,
                    review.ReviewTime,
                    review.StatusId);
            if (traineeAvailabilityExists)
                AddEntery("traineeId", "is not available");
        }
        /// <summary>
        /// checks reviewer availablity for review if reviewer is not available adds msg
        /// </summary>
        /// <param name="review"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckReviewerAvailablity(Review review)
        {
            if (review is null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            if (userExists && traineeExists && reviewStatusExists)
                reviewerAvailabilityExists =
                 ReviewerAvailabilityExists(
                    review.Id,
                    review.ReviewerId,
                    review.ReviewDate,
                    review.ReviewTime,
                    review.StatusId);
            if (reviewerAvailabilityExists)
                AddEntery("reviewerId", "is not available");
        }
        /// <summary>
        /// sets revieweExists to true if the review exists
        /// </summary>
        /// <param name="review"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckReviewExists(Review review)
        {
            if (review is null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            if (review.Id != 0)
                revieweExists = ReviewExists(review.Id);
        }
        /// <summary>
        /// validates review date
        /// </summary>
        /// <param name="reviewDate"></param>
        private void CheckReviewDate(DateTime reviewDate)
        {
            if (reviewDate.Date < DateTime.Now.Date)
                AddEntery(nameof(reviewDate), "Invalid Review Date");
        }
        /// <summary>
        /// validates review date
        /// </summary>
        /// <param name="reviewTime"></param>
        private void CheckReviewTime(DateTime reviewTime)
        {
            if (reviewTime < DateTime.Now && reviewTime.Date < DateTime.Now.Date)
                AddEntery(nameof(reviewTime), "Invalid Review Time");
        }
        /// <summary>
        /// validate review status
        /// </summary>
        /// <param name="statusId"></param>
        private void CheckStatus(int statusId)
        {
            reviewStatusExists = ReviewStatusExists(statusId);
            if (!reviewStatusExists)
                AddEntery(nameof(statusId), "Invalid Id");
        }
        /// <summary>
        /// checks if the topic name is available or not
        /// </summary>
        /// <param name="topic"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckForTopicNameAvailablelity(Topic topic)
        {
            if (topic is null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            isTopicNameAvailabe = IsTopicNameAvailabe(
                topic.TopicId,
                topic.CourseId,
                topic.Name!);
        }
        /// <summary>
        /// sets traineeFeedbackExists to true if the condition holds true
        /// </summary>
        /// <param name="feedback"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckTraineeFeedbackExists(TraineeFeedback feedback)
        {
            if (feedback is null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            if (courseExists && userExists && traineeExists)
                traineeFeedbackExists = TraineeFeedbackExists(
                    feedback.CourseId,
                    feedback.TraineeId,
                    feedback.TrainerId);
        }
        /// <summary>
        /// sets roleExists to true if the role exists
        /// </summary>
        /// <param name="roleId"></param>
        private void CheckRoleExists(int roleId)
        {
            roleExists = RoleExists(roleId);
        }
    }
}