using Microsoft.EntityFrameworkCore;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        public IEnumerable<Topic> GetTopicsByCourseId(int courseId,AppDbContext dbContext)
        {
            var courseExists = Validation.CourseExists(dbContext,courseId);
            if(courseExists) return dbContext.Topics.Where(t=>t.CourseId==courseId);
            else throw new ArgumentException("Invalid Id"); 
        }
        public Topic GetTopicById(int topicId,AppDbContext dbContext)
        {
            var topicExists = Validation.TopicExists(dbContext,topicId);
            if(topicExists)
            {
                var result = dbContext.Topics.Where(t=>t.TopicId==topicId).Include(t=>t.Attendances).Include(t=>t.Assignments).FirstOrDefault();
                if(result is not null) return result;
            }
            throw new ArgumentException("Invalid Id");
        }
        public Dictionary<string,string> CreateTopic(Topic topic,AppDbContext dbContext)
        {
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = Validation.ValidateTopic(topic,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpTopicDetails(topic);
                CreateAndSaveTopic(topic,dbContext);  
            }
            return validation;
        }
        public Dictionary<string,string> UpdateTopic(Topic topic,AppDbContext dbContext)
        {
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = Validation.ValidateTopic(topic,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbTopic = dbContext.Topics.Find(topic.TopicId);
                if(dbTopic is not null)
                {
                    SetUpTopicDetails(topic, dbTopic);
                    UpdateAndSaveTopic(dbTopic,dbContext);   
                }
            }
            return validation;
        }
        public bool DisableTopic(int topicId,AppDbContext dbContext)
        {
            var topicExists = Validation.TopicExists(dbContext,topicId);
            if(topicExists)
            {
                var dbTopic = dbContext.Topics.Find(topicId);
                if(dbTopic is not null)
                {
                    dbTopic.isDisabled = true;
                    dbTopic.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveTopic(dbTopic,dbContext);
                }
            }
            return topicExists;
        }
    }
}