using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public interface IValidation
    {
        bool AssignmentExists(int courseId, int topicId, int ownerId);
        bool AttendanceExists(int courseId, int topicId, int ownerId);
        bool CourseExists(int courseId);
        bool CourseFeedbackExists(int courseId, int traineeId);
        bool CourseUserExists(int courseId, int userId, int roleId);
        bool DepartmentExists(int? departmentId);
        bool IsCourseNameAvailable(int courseId, int departmentId, string name);
        bool IsTopicNameAvailabe(int topicId, int courseId, string name);
        bool MOMExists(int reviewId, int traineeId);
        bool MOMReviewExists(int reviewId, int traineeId);
        bool ReviewerAvailabilityExists(int id, int reviewerId, DateTime reviewDate, DateTime reviewTime, int statusId);
        bool ReviewExists(int reviewId);
        bool ReviewStatusExists(int statusId);
        bool RoleExists(int roleId);
        bool TopicExists(int topicId, int courseId);
        bool TopicExists(int topicId);
        bool TopicValidationExists(int courseId, string name);
        bool TraineeAvailabilityExists(int id, int traineeId, DateTime reviewDate, DateTime reviewTime, int statusId);
        bool TraineeFeedbackExists(int courseId, int traineeId, int trainerId);
        bool UserExists(int userId);
        bool UserExists(int userId, int roleId);
        bool UserExists(LoginModel user);
        Dictionary<string, string> ValidateAssignment(Assignment assignment);
        Dictionary<string, string> ValidateAttendance(Attendance attendance);
        Dictionary<string, string> ValidateCourse(Course course);
        Dictionary<string, string> ValidateCourseFeedback(CourseFeedback feedback);
        Dictionary<string, string> ValidateCourseUser(CourseUsers user);
        Dictionary<string, string> ValidateDepartment(Department dpet);
        Dictionary<string, string> ValidateLoginDetails(LoginModel user);
        Dictionary<string, string> ValidateMOM(MOM mom);
        Dictionary<string, string> ValidateReview(Review review);
        Dictionary<string, string> ValidateTopic(Topic topic);
        Dictionary<string, string> ValidateTraineeFeedback(TraineeFeedback feedback);
        Dictionary<string, string> ValidateUser(User user);
    }

    public partial class Validation : IValidation
    {
        public Validation(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        Dictionary<string, string> result = new Dictionary<string, string>();
        string fullNameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
        nameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z0\d])\2{2})\w[a-zA-Z&.#\d\s]*$",
        userNameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.\s]*$",
        emailValidation = @"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$",
        passwordValidation = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
        Image = @"^data:image\/[a-zA-Z]+;base64,",
        dateValidation = @"^\d{1,2}-\d{1,2}-\d{4}$",

        timeValidation = @"^(1[0-2]|0?[1-9]):([0-5]?[0-9])\s(●?[AP]M)?$",
        modeValidation = @"^((online)|(offline)|(Online)|(Offilne)){1}$",
        durationValidation = @"^(\d+ ((hr)|(hrs)|(mins)){1}$)|(\d+ ((hr)|(hrs)){1})\s([0-5][0-9] ((min)|(mins)){1})$",
        contentValidation = @"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$",
        feedbackValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.,#\s]{5,100}$",
        base64Validation = @"^data:application\/pdf;base64,";
        bool attendanceExists = false,
        assignmentExists = false,
        courseUsertExists = false,
        courseExists = false,
        isCourseNameAvailable = false,
        isTopicNameAvailabe = false,
        departmentExists = false,
        momExists = false,
        reviewExists = false,
        reviewStatusExists = false,
        topicExists = false,
        traineeFeedbackExists = false,
        courseFeedbackExists = false,
        userExists = false,
        revieweExists = false,
        reviewerAvailabilityExists = false,
        traineeAvailabilityExists = false,
        traineeExists = false,
        roleExists = false;
        private readonly AppDbContext dbContext;
    }

}