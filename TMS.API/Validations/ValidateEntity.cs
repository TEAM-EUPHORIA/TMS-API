using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public static partial class Validation
    {

        public static Dictionary<string, string> ValidateCourseUser(CourseUsers user,AppDbContext dbContext)
        {
            result.Clear();
            courseUsertExists=false;
            courseExists = CourseExists(dbContext,user.CourseId);
            userExists = UserExists(dbContext,user.UserId,user.RoleId);
            AddEnteryForCourseAndUser(courseExists,userExists,user.CourseId);
            if(result.Count==0)
            {
                if(courseExists && userExists) courseUsertExists = CourseUserExists(dbContext,user.CourseId,user.UserId,user.RoleId);
                if(courseUsertExists) AddEntery("Exists","true");
            }
            if(result.Count == 0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateAssignment(Assignment assignment,AppDbContext dbContext)
        {
            result.Clear();
            assignmentExists=false;
            checkIdForCourseTopicAndUser(assignment.CourseId,assignment.TopicId,assignment.OwnerId);
            validateAndAddEntery(assignment.Base64,base64Validation);

            if(result.Count == 0)
            {
                courseExists = CourseExists(dbContext, assignment.CourseId);
                topicExists = TopicExists(dbContext, assignment.TopicId);
                userExists = UserExists(dbContext, assignment.OwnerId);
                
                AddEnteryForCourseTopicAndUser(courseExists,topicExists,userExists);
                
                if(courseExists && topicExists && userExists)assignmentExists = AssignmentExists(dbContext,assignment.CourseId,assignment.TopicId,assignment.OwnerId);
                          
                if(assignmentExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateAttendance(Attendance attendance,AppDbContext dbContext)
        {
            result.Clear();
            attendanceExists=false;
            checkIdForCourseTopicAndUser(attendance.CourseId,attendance.TopicId,attendance.OwnerId);
            if(result.Count == 0)
            {
                courseExists = CourseExists(dbContext, attendance.CourseId);
                topicExists = TopicExists(dbContext, attendance.TopicId);
                userExists = UserExists(dbContext, attendance.OwnerId);
                
                AddEnteryForCourseTopicAndUser(courseExists,topicExists,userExists);

                if(courseExists && topicExists && userExists)attendanceExists = AttendanceExists(dbContext,attendance.CourseId,attendance.TopicId,attendance.OwnerId);
                                
                if(attendanceExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateLoginDetails(LoginModel user,AppDbContext dbContext)
        {            
            result.Clear();
            validateAndAddEntery(user.Email,emailValidation);
            validateAndAddEntery(user.Password,passwordValidation);
            if(result.Count == 0)
            {
                userExists = UserExists(dbContext,user);
                if(userExists) AddEntery("IsValid","true");
            }
            return result;
        }

         public static Dictionary<string, string> ValidateCourse(Course course,AppDbContext dbContext)
        {       
            result.Clear();
            courseValidationExists=false;
            checkIdAndAddEntery(course.TrainerId,nameof(course.TrainerId));
            checkIdAndAddEntery(course.DepartmentId,nameof(course.DepartmentId));
            validateAndAddEntery(course.Name,nameValidation);
            validateAndAddEntery(course.Duration,durationValidation);
            validateAndAddEntery(course.Description,contentValidation);
            
            if(result.Count == 0)
            {
                courseExists = CourseExists(dbContext,course.Id);
                userExists = UserExists(dbContext,course.TrainerId,3);
                departmentExists=DepartmentExists(dbContext,course.DepartmentId);          
                if(userExists && departmentExists)courseValidationExists=CourseValidateExists(dbContext,course.Id,course.DepartmentId,course.Name);   
                AddEnteryForCourseAndUserWithDept(courseExists,userExists,departmentExists,course.Id,courseValidationExists);
                if(courseExists && userExists && !courseValidationExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback,AppDbContext dbContext)
        {
            result.Clear();
            courseFeedbackExists=false;
            checkIdAndAddEntery(feedback.CourseId,nameof(feedback.CourseId));
            checkIdAndAddEntery(feedback.TraineeId,nameof(feedback.TraineeId));
            validateAndAddEntery(feedback.Feedback,feedbackValidation);
            if(feedback.Rating <= 0 || feedback.Rating > 5) AddEntery("feedback rating","rating must be between 0 to 5");
            if(result.Count==0)
            {
                courseExists = CourseExists(dbContext,feedback.CourseId);
                userExists = UserExists(dbContext,feedback.TraineeId,4);
                AddEnteryForCourseAndUser(courseExists,userExists,feedback.CourseId);
                if(courseExists && userExists) courseFeedbackExists = CourseFeedbackExists(dbContext,feedback.CourseId,feedback.TraineeId);
                if(courseFeedbackExists) AddEntery("Exists","true");
            }

            if(result.Count == 0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateDepartment(Department dpet,AppDbContext dbContext)
        {
            result.Clear();
            departmentExists=false;
            validateAndAddEntery(dpet.Name,userNameValidation);
            if(dpet.Id!=0)departmentExists=DepartmentExists(dbContext,dpet.Id);
            if(departmentExists)AddEntery("Exists","true");
            if(result.Count == 0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }

        public static Dictionary<string, string> ValidateMOM(MOM mom,AppDbContext dbContext)
        {
            result.Clear();
            momExists=false;
            checkIdAndAddEntery(mom.ReviewId,nameof(mom.ReviewId));
            checkIdAndAddEntery(mom.TraineeId,nameof(mom.TraineeId));
            validateAndAddEntery(mom.Agenda,contentValidation);
            validateAndAddEntery(mom.MeetingNotes,contentValidation);
            validateAndAddEntery(mom.PurposeOfMeeting,contentValidation);
            if(result.Count==0)
            {
                userExists = UserExists(dbContext,mom.TraineeId,4);
                reviewExists = MOMReviewExists(dbContext,mom.ReviewId,mom.TraineeId);
                if(userExists && reviewExists)momExists = MOMExists(dbContext,mom.ReviewId,mom.TraineeId);
                AddEnteryForUserAndReview(userExists,reviewExists,momExists);
                if(!momExists && revieweExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateReview(Review review,AppDbContext dbContext)
        {
            result.Clear();
            revieweExists=false;
            reviewerAvailabilityExists=false;
            traineeAvailabilityExists=false;
            checkIdAndAddEntery(review.ReviewerId,nameof(review.ReviewerId));
            checkIdAndAddEntery(review.TraineeId,nameof(review.TraineeId));
            checkIdAndAddEntery(review.StatusId,nameof(review.StatusId));
            validateAndAddEntery(review.ReviewDate.ToShortDateString(),dateValidation);
            validateAndAddEntery(review.ReviewTime.ToShortTimeString(),timeValidation);
            validateAndAddEntery(review.Mode,modeValidation);
            if(result.Count==0)
            {
                userExists = UserExists(dbContext,review.ReviewerId,5);
                traineeExists = UserExists(dbContext,review.TraineeId,4);
                reviewStatusExists = ReviewStatusExists(dbContext,review.StatusId);
                if(!reviewStatusExists) AddEntery(nameof(review.StatusId));
                
                if(review.ReviewDate<DateTime.UtcNow)AddEntery(nameof(review.ReviewDate));
                if(review.Id!=0)revieweExists= ReviewExists(dbContext,review.Id);
                if(userExists && traineeExists && reviewStatusExists)reviewerAvailabilityExists= ReviewerAvailabilityExists(dbContext,review.Id,review.ReviewerId,review.ReviewDate,review.ReviewTime,review.StatusId);
                if(reviewerAvailabilityExists)AddEntery(nameof(reviewerAvailabilityExists));
                if(userExists && traineeExists && reviewStatusExists)traineeAvailabilityExists= TraineeAvailabilityExists(dbContext,review.Id,review.TraineeId,review.ReviewDate,review.ReviewTime,review.StatusId);
                if(traineeAvailabilityExists)AddEntery(nameof(traineeAvailabilityExists));
                //AddEnteryForUsers(userExists,traineeExists);
                if(revieweExists && userExists && traineeExists && !reviewerAvailabilityExists && !traineeAvailabilityExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateTopic(Topic topic,AppDbContext dbContext)
        {
            result.Clear();
            topicExists=false;
            checkIdAndAddEntery(topic.CourseId,nameof(topic.CourseId));
            validateAndAddEntery(topic.Name,nameValidation);
            validateAndAddEntery(topic.Duration,durationValidation);
            if(result.Count==0) 
            {
                courseExists = CourseExists(dbContext,topic.CourseId);
                //if(!courseExists) AddEntery(nameof(topic.CourseId));
                if(courseExists && topic.TopicId != 0) topicExists = TopicExists(dbContext,topic.TopicId);
                topicValidationExists=TopicValidateExists(dbContext,topic.TopicId,topic.CourseId,topic.Name);
                AddEnteryForValidateTopic(topicValidationExists,courseExists,topic.CourseId);
                if(topicExists && !topicValidationExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback,AppDbContext dbContext)
        {
            result.Clear();
            traineeFeedbackExists=false;
            checkIdAndAddEntery(feedback.CourseId,nameof(feedback.CourseId));
            checkIdAndAddEntery(feedback.TraineeId,nameof(feedback.TraineeId));
            checkIdAndAddEntery(feedback.TrainerId,nameof(feedback.TrainerId));
            validateAndAddEntery(feedback.Feedback,feedbackValidation);
            if(result.Count==0)
            {
                courseExists = CourseExists(dbContext,feedback.CourseId);
                if(!courseExists) AddEntery(nameof(feedback.CourseId));
                userExists = UserExists(dbContext,feedback.TrainerId,3);
                traineeExists = UserExists(dbContext,feedback.TraineeId,4);
                AddEnteryForUsers(userExists,traineeExists);
                if(courseExists && userExists && traineeExists) traineeFeedbackExists = TraineeFeedbackExists(dbContext,feedback.CourseId,feedback.TraineeId,feedback.TrainerId);
                if(traineeFeedbackExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }
        public static Dictionary<string, string> ValidateUser(User user,AppDbContext dbContext)
        {
            result.Clear();
            if(user.RoleId<=dbContext.Roles.Count()) AddEntery(nameof(user.RoleId));
            if(user.DepartmentId<=dbContext.Departments.Count()) AddEntery(nameof(user.DepartmentId));
            validateAndAddEntery(user.FullName,fullNameValidation);
            validateAndAddEntery(user.UserName,userNameValidation);
            validateAndAddEntery(user.Email,emailValidation);
            validateAndAddEntery(user.Password,passwordValidation);
            validateAndAddEntery(user.Base64,Image);
             if(result.Count==0)
            {
                userExists = UserExists(dbContext,user.Id);
                if(userExists) AddEntery("Exists","true");
            }
            if(result.Count==0 || result.ContainsKey("Exists")) AddEntery("IsValid","true");
            return result;
        }

    }
}