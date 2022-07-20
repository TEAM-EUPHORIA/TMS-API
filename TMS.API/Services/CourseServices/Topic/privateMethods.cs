using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        /// <summary>
        /// used to setup topic Details.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="createdBy"></param>
        private static void SetUpTopicDetails(Topic topic,int createdBy)
        {
            topic.isDisabled = false;
            topic.CreatedOn = DateTime.Now;
            topic.CreatedBy = createdBy;
        }

        /// <summary>
        /// used to setup topic Details.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="dbTopic"></param>
        /// <param name="updatedBy"></param>
        private static void SetUpTopicDetails(Topic topic, Topic dbTopic,int updatedBy)
        {
            dbTopic.Name = topic.Name;
            dbTopic.Duration = topic.Duration;
            dbTopic.Content = topic.Content;
            dbTopic.UpdatedOn = DateTime.Now;
            dbTopic.UpdatedBy = updatedBy;
            dbTopic.Status = topic.Status;
        }

        /// <summary>
        /// used to disable the topic.
        /// </summary>
        /// <param name="dbTopic"></param>
        /// <param name="updatedBy"></param>
        private static void Disable(int updatedBy,Topic dbTopic)
        {
            dbTopic.isDisabled = true;
            dbTopic.UpdatedBy = updatedBy;
            dbTopic.UpdatedOn = DateTime.Now;
        }       
    }
}