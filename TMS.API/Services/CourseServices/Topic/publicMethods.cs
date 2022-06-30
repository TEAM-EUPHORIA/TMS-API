using Microsoft.EntityFrameworkCore;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService : ICourseService
    {
        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if (courseExists) return _repo.Courses.GetTopicsByCourseId(courseId);
            else throw new ArgumentException("Invalid Id");
        }
        public Topic GetTopicById(int courseId, int topicId, int userId)
        {
            var topicExists = _repo.Validation.TopicExists(topicId);
            if (topicExists)
            {
                return _repo.Courses.GetTopicById(courseId, topicId, userId);
            }
            throw new ArgumentException("Invalid Id");
        }
        public Dictionary<string, string> CreateTopic(Topic topic)
        {
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = _repo.Validation.ValidateTopic(topic);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpTopicDetails(topic);
                _repo.Courses.CreateTopic(topic);
                _repo.Complete();
            }
            return validation;
        }
        public Dictionary<string, string> UpdateTopic(Topic topic)
        {
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = _repo.Validation.ValidateTopic(topic);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbTopic = _repo.Courses.GetTopicById(topic.CourseId, topic.TopicId);
                SetUpTopicDetails(topic, dbTopic);
                _repo.Courses.UpdateTopic(dbTopic);
                _repo.Complete();
            }
            return validation;
        }
        public bool DisableTopic(int courseId, int topicId, int currentUserId)
        {
            var topicExists = _repo.Validation.TopicExists(topicId, courseId);
            if (topicExists)
            {
                var dbTopic = _repo.Courses.GetTopicById(courseId, topicId);
                disable(currentUserId, dbTopic);
                _repo.Complete();
            }
            return topicExists;
        }
        public Dictionary<string, string> MarkAttendance(Attendance attendance)
        {
            var validation = _repo.Validation.ValidateAttendance(attendance);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                _repo.Courses.MarkAttendance(attendance);
                _repo.Complete();
            }
            return validation;
        }
    }
}