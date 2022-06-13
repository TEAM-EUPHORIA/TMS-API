using Microsoft.EntityFrameworkCore;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            var courseExists = _repo.Validation.CourseExists(courseId);
            if(courseExists) return _repo.Courses.GetTopicsByCourseId(courseId);
            else throw new ArgumentException("Invalid Id"); 
        }
        public Topic GetTopicById(int courseId,int topicId)
        {
            var topicExists = _repo.Validation.TopicExists(topicId);
            if(topicExists)
            {
                return _repo.Courses.GetTopicById(courseId,topicId);
            }
            throw new ArgumentException("Invalid Id");
        }
        public Dictionary<string,string> CreateTopic(Topic topic)
        {
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = _repo.Validation.ValidateTopic(topic);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpTopicDetails(topic);
                _repo.Courses.CreateTopic(topic);
            }
            return validation;
        }
        public Dictionary<string,string> UpdateTopic(Topic topic)
        {
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = _repo.Validation.ValidateTopic(topic);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbTopic = _repo.Courses.GetTopicById(topic.CourseId,topic.TopicId);
                SetUpTopicDetails(topic, dbTopic);
                _repo.Courses.UpdateTopic(dbTopic);
            }
            return validation;
        }
        public bool DisableTopic(int courseId,int topicId)
        {
            var topicExists = _repo.Validation.TopicExists(topicId,courseId);
            if(topicExists)
            {
                _repo.Courses.DisableTopic(courseId,topicId);
            }
            return topicExists;
        }
    }
}