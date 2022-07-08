using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public partial class Validation
    {
        // validate course users
        // checks course exists
        // checks users exists
        public Dictionary<string, string> ValidateCourseUser(CourseUsers user)
        {
            courseExists = CourseExists(user.CourseId);
            userExists = UserExists(user.UserId,user.RoleId);
            AddEnteryValidateCourseUser(courseExists);
            if(result.Count == 0 && courseExists && userExists)
            {
                courseUsertExists = CourseUserExists(user.CourseId,user.UserId,user.RoleId);
                if(courseUsertExists) AddEntery("Exists","true");
            }
            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate assignment 
        // check course exists
        // check topic exists
        // check user exists
        public Dictionary<string, string> ValidateAssignment(Assignment assignment)
        {
            courseExists = CourseExists(assignment.CourseId);
            topicExists = TopicExists(assignment.TopicId);
            userExists = UserExists(assignment.OwnerId);
            AddEnteryValidateAssignment(courseExists,topicExists);
            if(result.Count == 0 && courseExists && topicExists && userExists)
            { 
                assignmentExists = AssignmentExists(assignment.CourseId,assignment.TopicId,assignment.OwnerId);          
                if(assignmentExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate attendance 
        // check course exists
        // check topic exists
        // check user exists
        public Dictionary<string, string> ValidateAttendance(Attendance attendance)
        {
            CheckIdForCourseTopicUser(attendance.CourseId,attendance.TopicId,attendance.OwnerId);
            if(result.Count == 0)
            {
                courseExists = CourseExists(attendance.CourseId);
                topicExists = TopicExists(attendance.TopicId);
                userExists = UserExists(attendance.OwnerId);
                AddEnteryValidateAttendance(courseExists,topicExists);
                if(courseExists && topicExists && userExists)
                {
                    if(attendance.Status == false){
                        attendance.Status = true;
                    };
                    attendanceExists = AttendanceExists(attendance.CourseId,attendance.TopicId,attendance.OwnerId);                   
                    if(attendanceExists) AddEntery("Exists","true");
                }
                AddEntery("IsValid","true");
            }
            return result;
        }
        // validate login model
        // regex validation for email, password
        public Dictionary<string, string> ValidateLoginDetails(LoginModel user)
        {            
            ValidateAndAddEntery(nameof(user.Email),user.Email,emailValidation);
            ValidateAndAddEntery(nameof(user.Password),user.Password,passwordValidation);
            if(result.Count == 0)
            {
                userExists = UserExists(user);
                if(userExists) AddEntery("IsValid","true");
            }
            return result;
        }
        // validate course model
        // check trainer exists
        // check department exists
        // check course name available
        // regex validation for Name, Duration, Description
        public Dictionary<string, string> ValidateCourse(Course course)
        {       
            CheckIdAndAddEntery(course.TrainerId,nameof(course.TrainerId));
            CheckIdAndAddEntery(course.DepartmentId,nameof(course.DepartmentId));
            ValidateAndAddEntery(nameof(course.Name),course.Name,nameValidation);
            ValidateAndAddEntery(nameof(course.Duration),course.Duration,durationValidation);
            ValidateAndAddEntery(nameof(course.Description),course.Description,contentValidation);
            
            if(result.Count == 0)
            {
                userExists = UserExists(course.TrainerId,3);
                if(course.Id != 0) 
                {
                    courseExists = CourseExists(course.Id);
                    AddEnteryCourseNotFound(courseExists);
                }
                departmentExists = DepartmentExists(course.DepartmentId);          
                if(userExists && departmentExists) isCourseNameAvailable=IsCourseNameAvailable(course.Id,course.DepartmentId,course.Name);   
                AddEnteryValidateCourse(userExists,departmentExists,isCourseNameAvailable);
                if(courseExists && userExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate course feedback model
        // check course exists
        // check trainee exists
        // rating validation
        // rexgex validation for feedback
        public Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback)
        {
            CheckIdAndAddEntery(feedback.CourseId,nameof(feedback.CourseId));
            CheckIdAndAddEntery(feedback.TraineeId,nameof(feedback.TraineeId));
            ValidateAndAddEntery(nameof(feedback.Feedback),feedback.Feedback,feedbackValidation);
            if(feedback.Rating <= 0 || feedback.Rating > 5) AddEntery("rating","rating must be between 0 to 5");
            if(result.Count == 0)
            {
                courseExists = CourseExists(feedback.CourseId);
                userExists = UserExists(feedback.TraineeId,4);
                AddEnteryValidateCourseFeedback(courseExists,userExists);
                if(courseExists && userExists) courseFeedbackExists = CourseFeedbackExists(feedback.CourseId,feedback.TraineeId);
                if(courseFeedbackExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate department
        // regex validation for name 
        public Dictionary<string, string> ValidateDepartment(Department dpet)
        {
            ValidateAndAddEntery(nameof(dpet.Name),dpet.Name,userNameValidation);
            if(dpet.Id != 0) departmentExists=DepartmentExists(dpet.Id);
            if(departmentExists && !result.ContainsKey("Exists"))AddEntery("Exists","true");
            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate Mom
        // check review exists
        // check trainee exists
        // regex validation for meeting notes, purpose if meeting, agenda
        public Dictionary<string, string> ValidateMOM(MOM mom)
        {
            CheckIdAndAddEntery(mom.ReviewId,nameof(mom.ReviewId));
            CheckIdAndAddEntery(mom.TraineeId,nameof(mom.TraineeId));
            ValidateAndAddEntery(nameof(mom.Agenda),mom.Agenda,contentValidation);
            ValidateAndAddEntery(nameof(mom.MeetingNotes),mom.MeetingNotes,contentValidation);
            ValidateAndAddEntery(nameof(mom.PurposeOfMeeting),mom.PurposeOfMeeting,contentValidation);
            if(result.Count==0)
            {
                userExists = UserExists(mom.TraineeId,4);
                reviewExists = MOMReviewExists(mom.ReviewId,mom.TraineeId);
                if(userExists && reviewExists) momExists = MOMExists(mom.ReviewId,mom.TraineeId);
                AddEnteryValidateMOM(userExists,reviewExists,momExists);
                if(momExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate Review model
        // check trainee exists
        // check reviewer exists
        // check review status exists
        // check availablity for trainee and reviewer
        // regex validation for review data, review time
        public Dictionary<string, string> ValidateReview(Review review)
        {
            CheckIdAndAddEntery(review.ReviewerId,nameof(review.ReviewerId));
            CheckIdAndAddEntery(review.TraineeId,nameof(review.TraineeId));
            CheckIdAndAddEntery(review.StatusId,nameof(review.StatusId));
            ValidateAndAddEntery(nameof(review.ReviewDate),review.ReviewDate.ToShortDateString(),dateValidation);
            ValidateAndAddEntery(nameof(review.ReviewTime),review.ReviewTime.ToString("hh:mm tt"),timeValidation);
            ValidateAndAddEntery(nameof(review.Mode),review.Mode,modeValidation);
            if(result.Count==0)
            {
                userExists = UserExists(review.ReviewerId, 5);
                traineeExists = UserExists(review.TraineeId, 4);
                reviewStatusExists = ReviewStatusExists(review.StatusId);
                CheckStatus(review.StatusId);
                CheckReviewDate(review);
                CheckReviewExists(review);
                CheckReviewerAvailablity(review);
                CheckTraineeAvailablity(review);
                if (revieweExists)
                    AddEntery("Exists", "true");
            }
            if (result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // check for trainee availablity 
        private void CheckTraineeAvailablity(Review review)
        {
            if (userExists && traineeExists && reviewStatusExists)
                traineeAvailabilityExists = TraineeAvailabilityExists(review.Id, review.TraineeId, review.ReviewDate, review.ReviewTime, review.StatusId);

            if (traineeAvailabilityExists)
                AddEntery("traineeId", "is not available");
        }
        // check for reviewer availablity 
        private void CheckReviewerAvailablity(Review review)
        {
            if (userExists && traineeExists && reviewStatusExists)
                reviewerAvailabilityExists = ReviewerAvailabilityExists(review.Id, review.ReviewerId, review.ReviewDate, review.ReviewTime, review.StatusId);

            if (reviewerAvailabilityExists)
                AddEntery("reviewerId", "is not available");
        }
        // check if review exists or not
        private void CheckReviewExists(Review review)
        {
            if (review.Id != 0)
                revieweExists = ReviewExists(review.Id);
        }
        // validation for review date
        private void CheckReviewDate(Review review)
        {
            if (review.ReviewDate < DateTime.Now)
                AddEntery(nameof(review.ReviewDate), "Invalid Date");
        }
        // used to add msg to result if the status is not found
        private void CheckStatus(int statusId)
        {
            if (!reviewStatusExists) AddEntery(nameof(statusId), "Invalid Id");
        }
        // validate Topic model
        // check course exists
        // check the topic name is available
        // regex validation for name and duration
        public Dictionary<string, string> ValidateTopic(Topic topic)
        {
            CheckIdAndAddEntery(topic.CourseId,nameof(topic.CourseId));
            ValidateAndAddEntery(nameof(topic.Name),topic.Name,nameValidation);
            ValidateAndAddEntery(nameof(topic.Duration),topic.Duration,durationValidation);
            if(result.Count==0) 
            {
                courseExists = CourseExists(topic.CourseId);
                if(courseExists && topic.TopicId != 0)
                {
                    topicExists = TopicExists(topic.TopicId,topic.CourseId);
                    AddEnteryTopicNotFound(topicExists);
                } 
                isTopicNameAvailabe = IsTopicNameAvailabe(topic.TopicId,topic.CourseId,topic.Name);
                AddEnteryValidateTopic(isTopicNameAvailabe,courseExists);
                if(topicExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate trainee feedback model
        // check course exists
        // check trainer exists
        // check trainee exists
        public Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback)
        {
            traineeFeedbackExists=false;
            CheckIdAndAddEntery(feedback.CourseId,nameof(feedback.CourseId));
            CheckIdAndAddEntery(feedback.TraineeId,nameof(feedback.TraineeId));
            CheckIdAndAddEntery(feedback.TrainerId,nameof(feedback.TrainerId));
            ValidateAndAddEntery(nameof(feedback.Feedback),feedback.Feedback,feedbackValidation);
            if(result.Count==0)
            {
                courseExists = CourseExists(feedback.CourseId);
                if(!courseExists) AddEntery(nameof(feedback.CourseId),"Invalid Data");
                userExists = UserExists(feedback.TrainerId,3);
                traineeExists = UserExists(feedback.TraineeId,4);
                AddEnteryValidateTraineeFeedback(courseExists,userExists,traineeExists);
                if(courseExists && userExists && traineeExists) traineeFeedbackExists = TraineeFeedbackExists(feedback.CourseId,feedback.TraineeId,feedback.TrainerId);
                if(traineeFeedbackExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        // validate user model
        // check role exists for user.RoleId
        // check department exists for user.DepartmentId if departmentId is not null
        // regex validation for Full name, User name, email, password, base64 image
        public Dictionary<string, string> ValidateUser(User user)
        {
            departmentExists = true;
            roleExists = RoleExists(user.RoleId);
            if(user.DepartmentId != null) departmentExists = DepartmentExists((int)user.DepartmentId);
            if(!roleExists) AddEntery(nameof(user.RoleId),"can't find the role");
            if(!departmentExists) AddEntery(nameof(user.DepartmentId),"can't find the department");
            if(result.Count==0 && roleExists)
            {
                ValidateAndAddEntery(nameof(user.FullName),user.FullName,fullNameValidation);
                ValidateAndAddEntery(nameof(user.UserName),user.UserName,userNameValidation);
                ValidateAndAddEntery(nameof(user.Email),user.Email,emailValidation);
                ValidateAndAddEntery(nameof(user.Password),user.Password,passwordValidation);
                ValidateAndAddEntery(nameof(user.Base64),user.Base64,Image);
                userExists = UserExists(user.Id);
                if(userExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }

    }
}