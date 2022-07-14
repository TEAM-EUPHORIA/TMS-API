using System.Text.RegularExpressions;
namespace TMS.API
{
    public partial class Validation
    {
        /// <summary>
        /// adds msg key: Exists, value: true
        /// </summary>
        private void AddEntryExists()
        {
            if(!result.ContainsKey("Exists"))
            AddEntery("Exists", "true");
        }
        /// <summary>
        /// adds msg Course with that name already exists
        /// </summary>
        private void CourseNameIsNotAvailable()
        {
            if(!result.ContainsKey("name"))
            AddEntery("name", "Course with that name already exists");
        }
        private void DepartmentNameIsNotAvailable()
        {
            if(!result.ContainsKey("name"))
            AddEntery("name", "Department with that name already exists");
        }
        /// <summary>
        /// adds msg can't find the trainer
        /// </summary>
        private void TrainerNotFound()
        {
            if(!result.ContainsKey("trainerId"))
            AddEntery("trainerId", "can't find the trainer");
        }
        /// <summary>
        /// adds msg can't find the course
        /// </summary>
        private void CourseNotFound()
        {
            if(!result.ContainsKey("courseId"))
            AddEntery("courseId", "can't find the course");
        }
        /// <summary>
        /// adds msg can't find the trainee
        /// </summary>
        private void TraineeNotFound()
        {
            if(!result.ContainsKey("traineeId"))
            AddEntery("traineeId", "can't find the trainee");
        }
        /// <summary>
        /// adds msg can't find the review
        /// </summary>
        private void ReviewNotFound()
        {
            if(!result.ContainsKey("reviewId"))
            AddEntery("reviewId", "can't find the review");
        }
        /// <summary>
        /// adds msg Topic with that name already exists
        /// </summary>
        private void TopicNameIsNotAvailable()
        {
            if(!result.ContainsKey("name"))
            AddEntery("name", "Topic with that name already exists");
        }
        /// <summary>
        /// adds msg Can't find the user
        /// </summary>
        private void OwnerNotFound()
        {
            if(!result.ContainsKey("ownerId"))
            AddEntery("ownerId", "Can't find the user");
        }
        /// <summary>
        /// adds msg Can't find the topic
        /// </summary>
        private void TopicNotFound()
        {
            if(!result.ContainsKey("topicId"))
            AddEntery("topicId", "Can't find the topic");
        }
        /// <summary>
        /// adds msg Can't find the department
        /// </summary>
        private void DepartmentNotFound()
        {
            if(!result.ContainsKey("departmentId"))
            AddEntery("departmentId", "can't find the department");
        }
        /// <summary>
        /// adds msg Can't find the role
        /// </summary>
        private void RoleNotFound()
        {
            if(!result.ContainsKey("roleId"))
            AddEntery("roleId", "can't find the role");
        }
        /// <summary>
        /// adds msg Can't find the user
        /// </summary>
        private void UserNotFound()
        {
            if(!result.ContainsKey("userId, roleId"))
            AddEntery("userId, roleId", "Can't find the user");
        }
        /// <summary>
        /// used to add msg to result Dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void AddEntery(string key, string value)
        {
            result.Add(key, value);
        }
        private void AddEntryForRoleAndDepartment()
        {
            if (!roleExists) RoleNotFound();
            if (!departmentExists) DepartmentNotFound();
        }
        private void AddEnteryValidateCourseUser(bool courseExists)
        {
            if (!courseExists)
                CourseNotFound();
            if (!userExists)
                UserNotFound();
        }
        private void AddEnteryValidateAssignment(bool courseExists, bool topicExists)
        {
            if (!courseExists)
                CourseNotFound();
            if (!topicExists)
                TopicNotFound();
            if (!userExists)
                OwnerNotFound();
        }
        private void AddEnteryValidateAttendance(bool courseExists, bool topicExists)
        {
            if (!courseExists)
                CourseNotFound();
            if (!topicExists)
                TopicNotFound();
            if (!userExists)
                OwnerNotFound();
        }
        private void AddEnteryValidateCourse(bool userExists, bool departmentExists, bool isCourseNameAvailable)
        {
            if (!userExists)
                TrainerNotFound();
            if (!departmentExists)
                DepartmentNotFound();
            if (isCourseNameAvailable)
                CourseNameIsNotAvailable();
        }
         private void AddEnteryValidateDepartment(bool isDepartmentNameAvailable)
        {
            if (isDepartmentNameAvailable)
                DepartmentNameIsNotAvailable();
        }
        private void AddEnteryValidateCourseFeedback(bool courseExists, bool userExists)
        {
            if (!userExists)
                TraineeNotFound();
            if (!courseExists)
                CourseNotFound();
        }
        private void AddEnteryValidateMOM(bool userExists, bool reviewExists, bool momExists)
        {
            if (!userExists)
                TraineeNotFound();
            if (!reviewExists)
                ReviewNotFound();
            
        }
        private void AddEnteryValidateTopic(bool isTopicNameAvailabe, bool courseExists)
        {
            if (!courseExists)
                CourseNotFound();
            if (isTopicNameAvailabe)
                TopicNameIsNotAvailable();
        }
        private void AddEnteryTopicNotFound(bool topicExists)
        {
            if (!topicExists)
                TopicNotFound();
        }
        private void AddEnteryValidateTraineeFeedback(bool courseExists, bool userExists, bool traineeExists)
        {
            if (!courseExists)
                CourseNotFound();
            if (!userExists)
                TrainerNotFound();
            if (!traineeExists)
                TraineeNotFound();
        }
        private void ValidateAndAddEntery(string key, string input, string regex)
        {
            if (!Regex.Match(input, regex).Success)
                AddEntery(key, "Invalid Data");
        }
        private void CheckIdAndAddEntery(int id, string key)
        {
            if (id == 0) AddEntery(key, "can't be zero");
        }
    }
}