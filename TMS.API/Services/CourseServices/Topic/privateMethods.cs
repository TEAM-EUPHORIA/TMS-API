using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private void SetUpTopicDetails(Topic topic)
        {
            topic.isDisabled = false;
            topic.CreatedOn = DateTime.Now;
        }

        private void SetUpTopicDetails(Topic topic, Topic dbTopic)
        {
            dbTopic.Name = topic.Name;
            dbTopic.Duration = topic.Duration;
            dbTopic.Content = topic.Content;
            dbTopic.UpdatedOn = DateTime.Now;
        }
        private void disable(int currentUserId,Topic dbTopic)
        {
            dbTopic.isDisabled = true;
            dbTopic.UpdatedBy = currentUserId;
            dbTopic.UpdatedOn = DateTime.Now;
        }       
    }
}