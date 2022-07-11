using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        private static void SetUpTopicDetails(Topic topic,int createdBy)
        {
            topic.isDisabled = false;
            topic.CreatedOn = DateTime.Now;
            topic.CreatedBy = createdBy;
        }

        private static void SetUpTopicDetails(Topic topic, Topic dbTopic,int updatedBy)
        {
            dbTopic.Name = topic.Name;
            dbTopic.Duration = topic.Duration;
            dbTopic.Content = topic.Content;
            dbTopic.UpdatedOn = DateTime.Now;
            dbTopic.UpdatedBy = updatedBy;
        }
        private static void Disable(int updatedBy,Topic dbTopic)
        {
            dbTopic.isDisabled = true;
            dbTopic.UpdatedBy = updatedBy;
            dbTopic.UpdatedOn = DateTime.Now;
        }       
    }
}