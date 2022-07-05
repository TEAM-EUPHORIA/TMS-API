using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public partial class Validation
    {
        public Dictionary<string, string> ValidateCourseUser(CourseUsers user)
        {
            courseExists = CourseExists(user.CourseId);
            userExists = UserExists(user.UserId,user.RoleId);
            AddEnteryValidateCourseUser(courseExists,userExists);
            if(result.Count == 0 && courseExists && userExists)
            {
                courseUsertExists = CourseUserExists(user.CourseId,user.UserId,user.RoleId);
                if(courseUsertExists) AddEntery("Exists","true");
            }
            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        public Dictionary<string, string> ValidateAssignment(Assignment assignment)
        {
            courseExists = CourseExists(assignment.CourseId);
            topicExists = TopicExists(assignment.TopicId);
            userExists = UserExists(assignment.OwnerId);
            AddEnteryValidateAssignment(courseExists,topicExists,userExists);
            if(result.Count == 0 && courseExists && topicExists && userExists)
            { 
                assignmentExists = AssignmentExists(assignment.CourseId,assignment.TopicId,assignment.OwnerId);          
                if(assignmentExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        public Dictionary<string, string> ValidateAttendance(Attendance attendance)
        {
            checkIdForCourseTopicUser(attendance.CourseId,attendance.TopicId,attendance.OwnerId);
            if(result.Count == 0)
            {
                courseExists = CourseExists(attendance.CourseId);
                topicExists = TopicExists(attendance.TopicId);
                userExists = UserExists(attendance.OwnerId);
                AddEnteryValidateAttendance(courseExists,topicExists,userExists);
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
        public Dictionary<string, string> ValidateLoginDetails(LoginModel user)
        {            
            validateAndAddEntery(nameof(user.Email),user.Email,emailValidation);
            validateAndAddEntery(nameof(user.Password),user.Password,passwordValidation);
            if(result.Count == 0)
            {
                userExists = UserExists(user);
                if(userExists) AddEntery("IsValid","true");
            }
            return result;
        }

        public Dictionary<string, string> ValidateCourse(Course course)
        {       
            checkIdAndAddEntery(course.TrainerId,nameof(course.TrainerId));
            checkIdAndAddEntery(course.DepartmentId,nameof(course.DepartmentId));
            validateAndAddEntery(nameof(course.Name),course.Name,nameValidation);
            validateAndAddEntery(nameof(course.Duration),course.Duration,durationValidation);
            validateAndAddEntery(nameof(course.Description),course.Description,contentValidation);
            
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
        public Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback)
        {
            checkIdAndAddEntery(feedback.CourseId,nameof(feedback.CourseId));
            checkIdAndAddEntery(feedback.TraineeId,nameof(feedback.TraineeId));
            validateAndAddEntery(nameof(feedback.Feedback),feedback.Feedback,feedbackValidation);
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
        public Dictionary<string, string> ValidateDepartment(Department dpet)
        {
            validateAndAddEntery(nameof(dpet.Name),dpet.Name,userNameValidation);
            if(dpet.Id != 0) departmentExists=DepartmentExists(dpet.Id);
            if(departmentExists && !result.ContainsKey("Exists"))AddEntery("Exists","true");
            if(result.Count == 0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }

        public Dictionary<string, string> ValidateMOM(MOM mom)
        {
            checkIdAndAddEntery(mom.ReviewId,nameof(mom.ReviewId));
            checkIdAndAddEntery(mom.TraineeId,nameof(mom.TraineeId));
            validateAndAddEntery(nameof(mom.Agenda),mom.Agenda,contentValidation);
            validateAndAddEntery(nameof(mom.MeetingNotes),mom.MeetingNotes,contentValidation);
            validateAndAddEntery(nameof(mom.PurposeOfMeeting),mom.PurposeOfMeeting,contentValidation);
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
        public Dictionary<string, string> ValidateReview(Review review)
        {
            checkIdAndAddEntery(review.ReviewerId,nameof(review.ReviewerId));
            checkIdAndAddEntery(review.TraineeId,nameof(review.TraineeId));
            checkIdAndAddEntery(review.StatusId,nameof(review.StatusId));
            validateAndAddEntery(nameof(review.ReviewDate),review.ReviewDate.ToShortDateString(),dateValidation);
            validateAndAddEntery(nameof(review.ReviewTime),review.ReviewTime.ToString("hh:mm tt"),timeValidation);
            validateAndAddEntery(nameof(review.Mode),review.Mode,modeValidation);
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

        private void CheckTraineeAvailablity(Review review)
        {
            if (userExists && traineeExists && reviewStatusExists)
                traineeAvailabilityExists = TraineeAvailabilityExists(review.Id, review.TraineeId, review.ReviewDate, review.ReviewTime, review.StatusId);

            if (traineeAvailabilityExists)
                AddEntery("traineeId", "is not available");
        }

        private void CheckReviewerAvailablity(Review review)
        {
            if (userExists && traineeExists && reviewStatusExists)
                reviewerAvailabilityExists = ReviewerAvailabilityExists(review.Id, review.ReviewerId, review.ReviewDate, review.ReviewTime, review.StatusId);

            if (reviewerAvailabilityExists)
                AddEntery("reviewerId", "is not available");
        }

        private void CheckReviewExists(Review review)
        {
            if (review.Id != 0)
                revieweExists = ReviewExists(review.Id);
        }

        private void CheckReviewDate(Review review)
        {
            if (review.ReviewDate < DateTime.Now)
                AddEntery(nameof(review.ReviewDate), "Invalid Date");
        }

        private void CheckStatus(int statusId)
        {
            if (!reviewStatusExists) AddEntery(nameof(statusId), "Invalid Id");
        }

        public Dictionary<string, string> ValidateTopic(Topic topic)
        {
            checkIdAndAddEntery(topic.CourseId,nameof(topic.CourseId));
            validateAndAddEntery(nameof(topic.Name),topic.Name,nameValidation);
            validateAndAddEntery(nameof(topic.Duration),topic.Duration,durationValidation);
            if(result.Count==0) 
            {
                courseExists = CourseExists(topic.CourseId);
                if(courseExists && topic.TopicId != 0)
                {
                    topicExists = TopicExists(topic.TopicId,topic.CourseId);
                    AddEnteryTopicNotFound(topicExists);
                } 
                isTopicNameAvailabe = IsTopicNameAvailabe(topic.TopicId,topic.CourseId,topic.Name);
                AddEnteryValidateTopic(isTopicNameAvailabe,courseExists,topicExists);
                if(topicExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }
        public Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback)
        {
            traineeFeedbackExists=false;
            checkIdAndAddEntery(feedback.CourseId,nameof(feedback.CourseId));
            checkIdAndAddEntery(feedback.TraineeId,nameof(feedback.TraineeId));
            checkIdAndAddEntery(feedback.TrainerId,nameof(feedback.TrainerId));
            validateAndAddEntery(nameof(feedback.Feedback),feedback.Feedback,feedbackValidation);
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
        public Dictionary<string, string> ValidateUser(User user)
        {
            departmentExists = true;
            roleExists = RoleExists(user.RoleId);
            if(user.DepartmentId != null) departmentExists = DepartmentExists((int)user.DepartmentId);
            if(!roleExists) AddEntery(nameof(user.RoleId),"can't find the role");
            if(!departmentExists) AddEntery(nameof(user.DepartmentId),"can't find the department");
            if(result.Count==0 && roleExists)
            {
                validateAndAddEntery(nameof(user.FullName),user.FullName,fullNameValidation);
                validateAndAddEntery(nameof(user.UserName),user.UserName,userNameValidation);
                validateAndAddEntery(nameof(user.Email),user.Email,emailValidation);
                validateAndAddEntery(nameof(user.Password),user.Password,passwordValidation);
                validateAndAddEntery(nameof(user.Base64),user.Base64,Image);
                userExists = UserExists(user.Id);
                if(userExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || (result.ContainsKey("Exists") && !result.ContainsKey("IsValid"))) AddEntery("IsValid","true");
            return result;
        }

    }
}