using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public static partial class Validation
    {
        static Dictionary<string,string> result = new Dictionary<string, string>();
        static string fullNameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$";
        static string nameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z0\d])\2{2})\w[a-zA-Z&.\d\s]*$";
        static string emailValidation = @"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$";
        static string passwordValidation = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        static string Image = @"^data:image\/[a-zA-Z]+;base64,";
        static string dateValidation = @"^\d{2}-\d{2}-\d{4}$";
        static string timeValidation = @"^(1[0-2]";
        static string modeValidation = @"^((online)|(offline)){1}$";
        static string durationValidation = @"^\d+ ((hr)|(hrs)|(mins)){1}$";
        static string contentValidation = @"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$";
        static string feedbackValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.,\s]{5,100}$";
        static string base64Validation = @"^data:application/pdf;base64,";
        private static void AddErrors(Dictionary<string, string> result, string propertyName)
        {
            result.Add($"{propertyName}", $"Invalid data.");
        }
        public static Dictionary<string, string> ValidateAssignment(Assignment assignment,AppDbContext dbContext)
        {
            if (!Regex.Match(assignment.Base64,base64Validation).Success) AddErrors(result, nameof(assignment.Base64));
            if (assignment.TopicId == 0) AddErrors(result, nameof(assignment.TopicId));
            if (assignment.OwnerId == 0) AddErrors(result, nameof(assignment.OwnerId));
            if (assignment.StatusId<=0 || assignment.StatusId>2) AddErrors(result,nameof(assignment.StatusId));
            if (result.Count == 0)
            {
                var topicExists = TopicExists(dbContext,assignment.TopicId);
                var userExists = UserExists(dbContext,assignment.OwnerId);
                if(topicExists && userExists){
                    result.Add("IsValid", "true");
                }
                if(!topicExists) AddErrors(result,nameof(assignment.TopicId));
                if(!userExists) AddErrors(result,nameof(assignment.OwnerId));
            }
            return result;
        }
        public static Dictionary<string, string> ValidateAttendance(Attendance attendance,AppDbContext dbContext)
        {
            if (attendance.TopicId == 0) AddErrors(result, nameof(attendance.TopicId));
            if (attendance.OwnerId == 0) AddErrors(result, nameof(attendance.OwnerId));
            if (attendance.StatusId<=0 || attendance.StatusId>2) AddErrors(result,nameof(attendance.StatusId));

            if (result.Count == 0) 
            {
                var topicExists = TopicExists(dbContext,attendance.TopicId);
                var userExists = UserExists(dbContext,attendance.OwnerId);
                if(topicExists && userExists){
                    result.Add("IsValid", "true");
                }
                if(!topicExists) AddErrors(result,nameof(attendance.TopicId));
                if(!userExists) AddErrors(result,nameof(attendance.OwnerId));
            }

            return result;
        }
        public static Dictionary<string, string> ValidateLoginDetails(LoginModel user,AppDbContext dbContext)
        {            
            if (!Regex.Match(user.Email,emailValidation).Success) AddErrors(result, nameof(user.Email));
            if (!Regex.Match(user.Password,passwordValidation).Success) AddErrors(result, nameof(user.Password));

            if (result.Count == 0)
            {
                var userExists = UserExists(dbContext,user);
                if(userExists)
                {
                    result.Add("IsValid", "true");
                }
            } 

            return result;
        }

        public static Dictionary<string, string> ValidateCourse(Course course,AppDbContext dbContext)
        {       
            if (!Regex.Match(course.Name,nameValidation).Success) AddErrors(result, nameof(course.Name));
            if (!Regex.Match(course.Description,contentValidation).Success) AddErrors(result, nameof(course.Description));
            if (!Regex.Match(course.Duration,durationValidation).Success) AddErrors(result, nameof(course.Duration));
            if (course.TrainerId == 0) AddErrors(result,nameof(course.TrainerId));
            if (result.Count == 0)
            {
                var userExists = UserExists(dbContext,course.TrainerId);
                result.Add("IsValid", "true");
            }

            return result;
        }
        public static Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback,AppDbContext dbContext)
        {
            result.Clear();
            if (!Regex.Match(feedback.Feedback,feedbackValidation).Success) AddErrors(result, nameof(feedback.Feedback));
            if (feedback.Rating <= 0.0 || feedback.Rating > 5) AddErrors(result, nameof(feedback.Rating));
            if (feedback.CourseId == 0) AddErrors(result, nameof(feedback.CourseId));
            if (feedback.OwnerId == 0) AddErrors(result, nameof(feedback.OwnerId));

            if (result.Count == 0) 
            {
                var courseExists = CourseExists(dbContext,feedback.CourseId);
                var userExists = UserExists(dbContext,feedback.OwnerId);
                bool feedbackExits = false;
                if(feedback.Id==0)
                {
                  feedbackExits=FeedbackExists(dbContext,feedback.CourseId,feedback.OwnerId);
                }
                else
                {
                    feedbackExits = false;
                }
                if(courseExists && userExists)
                {
                    result.Add("IsValid", "true");
                }
                if(!courseExists) AddErrors(result,nameof(feedback.CourseId));
                if(!userExists) AddErrors(result,nameof(feedback.OwnerId));
                if(feedbackExits)
                {
                 result.Clear();   
                 result.Add("Message","Feedback Already Exists");
                }
            }

            return result;
        }
        public static Dictionary<string, string> ValidateDepartment(Department dpet)
        {
            if (!Regex.Match(dpet.Name,nameValidation).Success) AddErrors(result, nameof(dpet.Name));
            
            if (result.Count == 0) result.Add("IsValid", "true");
            
            return result;
        }

        public static Dictionary<string, string> ValidateMOM(MOM mom,AppDbContext dbContext)
        {
            if (!Regex.Match(mom.Agenda,contentValidation).Success) AddErrors(result, nameof(mom.Agenda));
            if (!Regex.Match(mom.MeetingNotes,contentValidation).Success) AddErrors(result, nameof(mom.MeetingNotes));
            if (!Regex.Match(mom.PurposeOfMeeting,contentValidation).Success) AddErrors(result, nameof(mom.PurposeOfMeeting));
            if (mom.ReviewId == 0) AddErrors(result, nameof(mom.ReviewId));
            if (mom.OwnerId == 0) AddErrors(result, nameof(mom.OwnerId));
            if (mom.StatusId <= 0 || mom.StatusId > 2) AddErrors(result,nameof(mom.StatusId));
            if (result.Count == 0) 
            {
                var reviewExists = ReviewExists(dbContext,mom.ReviewId);
                var ownerExists = UserExists(dbContext,mom.OwnerId);
                if(reviewExists && ownerExists)
                {
                    result.Add("IsValid", "true");
                }
                if(!reviewExists) AddErrors(result,nameof(mom.ReviewId));
                if(!ownerExists)  AddErrors(result,nameof(mom.OwnerId));
            }

            return result;
        }
        public static Dictionary<string, string> ValidateReview(Review review,AppDbContext dbContext)
        {

            //if (!Regex.Match(review.ReviewDate,dateValidation).Success) AddErrors(result, nameof(review.ReviewDate));
            //if (!Regex.Match(review.ReviewTime,timeValidation).Success) AddErrors(result, nameof(review.ReviewTime));
            if (!Regex.Match(review.Mode,modeValidation).Success) AddErrors(result, nameof(review.Mode));
            if (review.ReviewerId == 0) AddErrors(result, nameof(review.ReviewerId));
            if (review.TraineeId == 0) AddErrors(result, nameof(review.TraineeId));
            if (review.StatusId <= 0 && review.StatusId > 3) AddErrors(result, nameof(review.StatusId));

            if (result.Count == 0) 
            {
                var reviewerExists = UserExists(dbContext,review.ReviewerId);
                var traineeExists = UserExists(dbContext,review.TraineeId);
                if(reviewerExists && traineeExists)
                {
                    result.Add("IsValid", "true");
                }
                if(!reviewerExists) AddErrors(result,nameof(review.ReviewerId));
                if(!traineeExists)  AddErrors(result,nameof(review.TraineeId));
            }
            
            return result;
        }
        public static Dictionary<string, string> ValidateRole(Role role)
        {
            if (!Regex.Match(role.Name,nameValidation).Success) AddErrors(result, nameof(role.Name));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateTopic(Topic topic,AppDbContext dbContext)
        {
            if (!Regex.Match(topic.Name,nameValidation).Success) AddErrors(result, nameof(topic.Name));
            if (!Regex.Match(topic.Content,contentValidation).Success) AddErrors(result, nameof(topic.Content));
            if (!Regex.Match(topic.Duration,durationValidation).Success) AddErrors(result, nameof(topic.Duration));

            if (result.Count == 0) 
            {
                var courseExists = CourseExists(dbContext,topic.CourseId);
                if(courseExists)
                {
                    result.Add("IsValid", "true");
                }
            }

            return result;
        }
        public static Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback,AppDbContext dbContext)
        {
            if (!Regex.Match(feedback.Feedback,feedbackValidation).Success) AddErrors(result, nameof(feedback.Feedback));
            if (feedback.TraineeId == 0 ) AddErrors(result, nameof(feedback.TraineeId));
            if (feedback.TrainerId == 0) AddErrors(result, nameof(feedback.TrainerId));
            if (feedback.CourseId == 0) AddErrors(result, nameof(feedback.CourseId));
            if (feedback.StatusId <= 0 || feedback.StatusId > 2) AddErrors(result, nameof(feedback.StatusId));

            if (result.Count == 0) 
            {
                var traineeExists = UserExists(dbContext,feedback.TraineeId);
                var trainerExists = UserExists(dbContext,feedback.TrainerId);
                var courseExists = CourseExists(dbContext,feedback.CourseId);

                if(traineeExists && trainerExists && courseExists)
                {
                    result.Add("IsValid", "true");
                }

                if(!trainerExists) AddErrors(result,nameof(feedback.TrainerId));
                if(!traineeExists) AddErrors(result,nameof(feedback.TraineeId));
                if(!courseExists) AddErrors(result,nameof(feedback.CourseId));
            }

            return result;
        }
        public static Dictionary<string, string> ValidateUser(User user)
        {
            if (!Regex.Match(user.UserName,nameValidation).Success) AddErrors(result, nameof(user.UserName));
            if (!Regex.Match(user.FullName,fullNameValidation).Success) AddErrors(result, nameof(user.FullName));
            if (!Regex.Match(user.Email,emailValidation).Success) AddErrors(result, nameof(user.Email));
            if (!Regex.Match(user.Password,passwordValidation).Success) AddErrors(result, nameof(user.Password));
            if (user.RoleId <= 0 || user.RoleId > 6) AddErrors(result, nameof(user.RoleId));
            if (user.DepartmentId != null && (user.DepartmentId <= 0 || user.DepartmentId > 3)) AddErrors(result, nameof(user.DepartmentId));
            if (!Regex.Match(user.Base64,Image).Success) AddErrors(result, nameof(user.Base64));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }

    }
}