using System.Text.RegularExpressions;

namespace TMS.API
{
    public partial class Validation
    {
        private void AddEntery(string key, string value)
        {
            result.Add(key, value);
        }
        private void AddEnteryValidateCourseUser(bool courseExists)
        {
            AddEnteryCourseNotFound(courseExists);
            if (!userExists) AddEntery("userId, roleId", "Can't find the user");
        }
        private void AddEnteryValidateAssignment(bool courseExists, bool topicExists)
        {
            AddEnteryCourseNotFound(courseExists);
            if (!topicExists) AddEntery("topicId", "Can't find the topic");
            if (!userExists) AddEntery("ownerId", "Can't find the user");
        }
        private void AddEnteryValidateAttendance(bool courseExists, bool topicExists)
        {
            AddEnteryCourseNotFound(courseExists);
            if (!topicExists) AddEntery("topicId", "Can't find the topic");
            if (!userExists) AddEntery("ownerId", "Can't find the user");
        }
        private void AddEnteryValidateCourse(bool userExists, bool departmentExists, bool isCourseNameAvailable)
        {
            if (!userExists) AddEntery("trainerId", "can't find the user");
            if (!departmentExists) AddEntery("departmentId", "can't find the department");
            if (isCourseNameAvailable) AddEntery("name", "Course with that name already exists");
        }

        private void AddEnteryCourseNotFound(bool courseExists)
        {
            if (!courseExists) AddEntery("courseId", "can't find the course");
        }

        private void AddEnteryValidateCourseFeedback(bool courseExists, bool userExists)
        {
            if (!userExists) AddEntery("traineeId", "can't find the user");
            AddEnteryCourseNotFound(courseExists);
        }
        private void AddEnteryValidateMOM(bool userExists, bool reviewExists, bool momExists)
        {
            if (!userExists) AddEntery("traineeId", "can't find the user");
            if (!reviewExists) AddEntery("reviewId", "can't find the review");
            if (momExists) AddEntery("mom", "mom already exists");
        }
        private void AddEnteryValidateTopic(bool isTopicNameAvailabe, bool courseExists)
        {
            AddEnteryCourseNotFound(courseExists);
            if (isTopicNameAvailabe) AddEntery("name", "Topic with that name already exists");
        }

        private void AddEnteryTopicNotFound(bool topicExists)
        {
            if (!topicExists) AddEntery("topicId", "can't find the topic");
        }

        private void AddEnteryValidateTraineeFeedback(bool courseExists, bool userExists, bool traineeExists)
        {
            AddEnteryCourseNotFound(courseExists);
            if (!userExists) AddEntery("trainerId", "can't find the user");
            if (!traineeExists) AddEntery("traineeId", "can't find the user");
        }
        private void CheckIdForCourseTopicUser(int courseId, int topicId, int ownerId)
        {
            if (courseId == 0) AddEntery("courseId", "can't be zero");
            if (topicId == 0) AddEntery("topicId", "can't be zero");
            if (ownerId == 0) AddEntery("ownerId", "can't be zero");
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