using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API
{
    public partial class Validation
    {
        /// <summary>
        /// used to validate the course user entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateCourseUser(CourseUsers user)
        {
            CheckCourseExists(user.CourseId);
            CheckUserExists(user.UserId, user.RoleId);
            AddEnteryValidateCourseUser(courseExists);
            CheckCourseUserExists(user);
            if (courseUsertExists)
                AddEntryExists();
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the assignment entity 
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateAssignment(Assignment assignment)
        {
            CheckCourseExists(assignment.CourseId);
            CheckTopicExists(assignment.TopicId);
            CheckUserExists(assignment.OwnerId);
            AddEnteryValidateAssignment(courseExists, topicExists);
            CheckAssignmentExists(assignment);
            if (assignmentExists)
                AddEntryExists();
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the attendance entity 
        /// </summary>
        /// <param name="attendance"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateAttendance(Attendance attendance)
        {
            CheckCourseExists(attendance.CourseId);
            CheckTopicExists(attendance.TopicId);
            CheckUserExists(attendance.OwnerId);
            AddEnteryValidateAttendance(courseExists, topicExists);
            CheckAttendanceExists(attendance);
            if (attendanceExists)
                AddEntryExists();
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the Login user model 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateLoginDetails(LoginModel user)
        {
            ValidateAndAddEntery(nameof(user.Email), user.Email, emailValidation);
            ValidateAndAddEntery(nameof(user.Password), user.Password, passwordValidation);
            CheckUserExists(user);
            if (userExists)
                CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the course entity 
        /// </summary>
        /// <param name="course"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateCourse(Course course)
        {
            CheckIdAndAddEntery(course.TrainerId, nameof(course.TrainerId));
            CheckIdAndAddEntery(course.DepartmentId, nameof(course.DepartmentId));
            ValidateAndAddEntery(nameof(course.Name), course.Name, nameValidation);
            ValidateAndAddEntery(nameof(course.Duration), course.Duration, durationValidation);
            ValidateAndAddEntery(nameof(course.Description), course.Description, contentValidation);
            if (result.Count == 0)
            {
                CheckUserExists(course.TrainerId, 3);
                CheckCourseIfIdIsNotZero(course);
                CheckDepartmentExists(course.DepartmentId);
                CheckForCourseNameAvailablity(course);
                AddEnteryValidateCourse(userExists, departmentExists, isCourseNameAvailable);
                if (courseExists && userExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the course feedback entity 
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback)
        {
            CheckIdAndAddEntery(feedback.CourseId, nameof(feedback.CourseId));
            CheckIdAndAddEntery(feedback.TraineeId, nameof(feedback.TraineeId));
            ValidateAndAddEntery(nameof(feedback.Feedback), feedback.Feedback, feedbackValidation);
            CheckIfRatingIsValid(feedback.Rating);
            if (result.Count == 0)
            {
                CheckCourseExists(feedback.CourseId);
                CheckUserExists(feedback.TraineeId, 4);
                AddEnteryValidateCourseFeedback(courseExists, userExists);
                CheckCourseFeedbackExists(feedback);
                if (courseFeedbackExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the department entity 
        /// </summary>
        /// <param name="department"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateDepartment(Department department)
        {
            ValidateAndAddEntery(nameof(department.Name), department.Name, userNameValidation);
            
            CheckDepartmentExistsIfIdIsNotZero(department.Id);
            CheckForDepartmentNameAvailablity(department);
                AddEnteryValidateDepartment(isDepartMentNameAvailable);
            if (departmentExists)
                AddEntryExists();
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the MOM entity 
        /// </summary>
        /// <param name="mom"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateMOM(MOM mom)
        {
            CheckIdAndAddEntery(mom.ReviewId, nameof(mom.ReviewId));
            CheckIdAndAddEntery(mom.TraineeId, nameof(mom.TraineeId));
            ValidateAndAddEntery(nameof(mom.Agenda), mom.Agenda, contentValidation);
            ValidateAndAddEntery(nameof(mom.MeetingNotes), mom.MeetingNotes, contentValidation);
            ValidateAndAddEntery(nameof(mom.PurposeOfMeeting), mom.PurposeOfMeeting, contentValidation);
            if (result.Count == 0)
            {
                CheckUserExists(mom.TraineeId, 4);
                CheckMomReviewExists(mom);
                CheckMomExists(mom);
                AddEnteryValidateMOM(userExists, reviewExists, momExists);
                if (momExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the review entity 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateReview(Review review)
        {
            CheckIdAndAddEntery(review.ReviewerId, nameof(review.ReviewerId));
            CheckIdAndAddEntery(review.TraineeId, nameof(review.TraineeId));
            CheckIdAndAddEntery(review.StatusId, nameof(review.StatusId));
            ValidateAndAddEntery(nameof(review.ReviewDate), review.ReviewDate.ToShortDateString(), dateValidation);
            ValidateAndAddEntery(nameof(review.ReviewTime), review.ReviewTime.ToString("hh:mm tt"), timeValidation);
            ValidateAndAddEntery(nameof(review.Mode), review.Mode, modeValidation);
            if (result.Count == 0)
            {
                CheckUserExists(review.ReviewerId, 5);
                CheckTraineeExists(review.TraineeId);
                CheckStatus(review.StatusId);
                CheckReviewDate(review.ReviewDate);
                CheckReviewerAvailablity(review);
                CheckTraineeAvailablity(review);
                CheckReviewExists(review);
                if (revieweExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the topic entity 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateTopic(Topic topic)
        {
            CheckIdAndAddEntery(topic.CourseId, nameof(topic.CourseId));
            ValidateAndAddEntery(nameof(topic.Name), topic.Name, nameValidation);
            ValidateAndAddEntery(nameof(topic.Duration), topic.Duration, durationValidation);
            if (result.Count == 0)
            {
                CheckCourseExists(topic.CourseId);
                if (courseExists && topic.TopicId != 0)
                {
                    CheckTopicExists(topic.TopicId, topic.CourseId);
                    AddEnteryTopicNotFound(topicExists);
                }
                CheckForTopicNameAvailablelity(topic);
                AddEnteryValidateTopic(isTopicNameAvailabe, courseExists);
                if (topicExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the trainee feedback entity 
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback)
        {
            CheckIdAndAddEntery(feedback.CourseId, nameof(feedback.CourseId));
            CheckIdAndAddEntery(feedback.TraineeId, nameof(feedback.TraineeId));
            CheckIdAndAddEntery(feedback.TrainerId, nameof(feedback.TrainerId));
            ValidateAndAddEntery(nameof(feedback.Feedback), feedback.Feedback, feedbackValidation);
            if (result.Count == 0)
            {
                CheckCourseExists(feedback.CourseId);
                CheckUserExists(feedback.TrainerId, 3);
                CheckTraineeExists(feedback.TraineeId);
                AddEnteryValidateTraineeFeedback(courseExists, userExists, traineeExists);
                CheckTraineeFeedbackExists(feedback);
                if (traineeFeedbackExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
        /// <summary>
        /// used to validate the user entity 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// returns a Dictionary
        /// </returns>
        public Dictionary<string, string> ValidateUser(User user)
        {
            departmentExists = true;
            CheckRoleExists(user.RoleId);
            if (user.DepartmentId != null) CheckDepartmentExists((int)user.DepartmentId);
            AddEntryForRoleAndDepartment();
            if (result.Count == 0 && roleExists)
            {
                ValidateAndAddEntery(nameof(user.FullName), user.FullName, fullNameValidation);
                ValidateAndAddEntery(nameof(user.UserName), user.UserName, userNameValidation);
                ValidateAndAddEntery(nameof(user.Email), user.Email, emailValidation);
                ValidateAndAddEntery(nameof(user.Password), user.Password, passwordValidation);
                ValidateAndAddEntery(nameof(user.Base64), user.Base64, Image);
                CheckUserExists(user.Id);
                // CheckDepartmentExists((int)user.DepartmentId);
                 CheckForUserMailAvailablity(user);
                 AddEnteryValidateUser(isUserMailAvailable);
                if (userExists)
                    AddEntryExists();
            }
            CheckIsValid();
            return result;
        }
    }
}