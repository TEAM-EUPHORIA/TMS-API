using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMS.BAL;

namespace TMS.API
{
    public static class Validation
    {
        private static void AddErrors(Dictionary<string, string> result, string propertyName)
        {
            result.Add($"{propertyName}", $"Invalid data.");
        }
        public static Dictionary<string, string> ValidateAssignment(Assignment assignment)
        {
            var result = new Dictionary<string, string>();
            var base64Validation = new Regex(@"^data:application/pdf;base64,");
            if (!base64Validation.IsMatch(assignment.Base64)) AddErrors(result, nameof(assignment.Base64));
            if (assignment.TopicId == 0) AddErrors(result, nameof(assignment.TopicId));
            if (assignment.OwnerId == 0) AddErrors(result, nameof(assignment.OwnerId));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateAttendance(Attendance attendance)
        {
            var result = new Dictionary<string, string>();
            if (attendance.TopicId == 0) AddErrors(result, nameof(attendance.TopicId));
            if (attendance.OwnerId == 0) AddErrors(result, nameof(attendance.OwnerId));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }

        public static Dictionary<string, string> ValidateCourse(Course course)
        {
            var result = new Dictionary<string, string>();

            var nameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z&.\s]*$");
            var durationValidation = new Regex(@"^\d+ ((hr)|(hrs)|(mins)){1}$");
            var descriptionValidation = new Regex(@"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$");
            if (!nameValidation.IsMatch(course.Name)) AddErrors(result, nameof(course.Name));
            if (!descriptionValidation.IsMatch(course.Description)) AddErrors(result, nameof(course.Description));
            if (!durationValidation.IsMatch(course.Duration)) AddErrors(result, nameof(course.Duration));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback)
        {
            var result = new Dictionary<string, string>();
            var feedbackValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.,\s]*{5,100}$");
            if (!feedbackValidation.IsMatch(feedback.Feedback)) AddErrors(result, nameof(feedback.Feedback));
            if (feedback.Rating <= 0.0 || feedback.Rating > 5) AddErrors(result, nameof(feedback.Rating));
            if (feedback.CourseId == 0) AddErrors(result, nameof(feedback.CourseId));
            if (feedback.OwnerId == 0) AddErrors(result, nameof(feedback.OwnerId));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateDepartment(Department dpet)
        {
            var result = new Dictionary<string, string>();
            var nameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$");
            if (!nameValidation.IsMatch(dpet.Name)) AddErrors(result, nameof(dpet.Name));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }

        public static Dictionary<string, string> ValidateMOM(MOM mom)
        {
            var result = new Dictionary<string, string>();

            var contentValidation = new Regex(@"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$");
            if (!contentValidation.IsMatch(mom.Agenda)) AddErrors(result, nameof(mom.Agenda));
            if (!contentValidation.IsMatch(mom.MeetingNotes)) AddErrors(result, nameof(mom.MeetingNotes));
            if (!contentValidation.IsMatch(mom.PurposeOfMeeting)) AddErrors(result, nameof(mom.PurposeOfMeeting));
            if (mom.ReviewId == 0) AddErrors(result, nameof(mom.ReviewId));
            if (mom.OwnerId == 0) AddErrors(result, nameof(mom.OwnerId));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateReview(Review review)
        {
            var result = new Dictionary<string, string>();

            var dateValidation = new Regex(@"^\d{2}-[a-zA-Z]{3}-\d{4}$");
            var timeValidation = new Regex(@"^(1[0-2]|0?[1-9]):([0-5]?[0-9])(●?[AP]M)?$");
            var modeValidation = new Regex(@"^((online)|(offline)){1}$");
            if (!dateValidation.IsMatch(review.ReviewDate)) AddErrors(result, nameof(review.ReviewDate));
            if (!timeValidation.IsMatch(review.ReviewTime)) AddErrors(result, nameof(review.ReviewTime));
            if (!modeValidation.IsMatch(review.Mode)) AddErrors(result, nameof(review.Mode));
            if (review.ReviewerId == 0) AddErrors(result, nameof(review.ReviewerId));
            if (review.TraineeId == 0) AddErrors(result, nameof(review.TraineeId));
            if (review.StatusId == 0) AddErrors(result, nameof(review.StatusId));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateRole(Role role)
        {
            var result = new Dictionary<string, string>();
            var nameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$");
            if (!nameValidation.IsMatch(role.Name)) AddErrors(result, nameof(role.Name));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateTopic(Topic topic)
        {
            var result = new Dictionary<string, string>();

            var nameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z&.\s]*$");
            var durationValidation = new Regex(@"^\d+ ((hr)|(hrs)|(mins)){1}$");
            var contentValidation = new Regex(@"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$");
            if (!nameValidation.IsMatch(topic.Name)) AddErrors(result, nameof(topic.Name));
            if (!contentValidation.IsMatch(topic.Content)) AddErrors(result, nameof(topic.Content));
            if (!durationValidation.IsMatch(topic.Duration)) AddErrors(result, nameof(topic.Duration));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback)
        {
            var result = new Dictionary<string, string>();
            var feedbackValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.,\s]*{5,100}$");
            if (!feedbackValidation.IsMatch(feedback.Feedback)) AddErrors(result, nameof(feedback.Feedback));
            if (feedback.TraineeId == 0 ) AddErrors(result, nameof(feedback.TraineeId));
            if (feedback.TrainerId == 0) AddErrors(result, nameof(feedback.TrainerId));
            if (feedback.CourseId == 0) AddErrors(result, nameof(feedback.CourseId));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }

        public static Dictionary<string, string> ValidateStatus(IdAndStatus status)
        {
            var result = new Dictionary<string, string>();
            var nameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$");
            if (!nameValidation.IsMatch(status.Status)) AddErrors(result, nameof(status.Status));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }
        public static Dictionary<string, string> ValidateUser(User user)
        {
            var result = new Dictionary<string, string>();

            var emailValidation = new Regex(@"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$");
            var fullNameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$");
            var userNameValidation = new Regex(@"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s.]*$");
            var passwordValidation = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            var base64Validation = new Regex(@"^data:image\/[a-zA-Z]+;base64,");

            if (!userNameValidation.IsMatch(user.UserName)) AddErrors(result, nameof(user.UserName));
            if (!fullNameValidation.IsMatch(user.FullName)) AddErrors(result, nameof(user.FullName));
            if (!emailValidation.IsMatch(user.Email)) AddErrors(result, nameof(user.Email));
            if (!passwordValidation.IsMatch(user.Password)) AddErrors(result, nameof(user.Password));
            if (user.RoleId <= 0 || user.RoleId > 6) AddErrors(result, nameof(user.RoleId));
            if (user.DepartmentId != null && (user.DepartmentId <= 0 || user.DepartmentId > 3)) AddErrors(result, nameof(user.DepartmentId));
            if (!base64Validation.Match(user.Base64).Success) AddErrors(result, nameof(user.Base64));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }

    }
}