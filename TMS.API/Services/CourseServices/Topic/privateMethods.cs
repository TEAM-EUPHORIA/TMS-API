using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private void SetUpTopicDetails(Topic topic)
        {
            topic.isDisabled = false;
            topic.CreatedOn = DateTime.UtcNow;
        }

        private void SetUpTopicDetails(Topic topic, Topic dbTopic)
        {
            dbTopic.Name = topic.Name;
            dbTopic.Duration = topic.Duration;
            dbTopic.Content = topic.Content;
            dbTopic.UpdatedOn = DateTime.UtcNow;
        }      
    }
}