using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService
    {
        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
           if (courseId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetTopicsByCourseId));
            try
            {
                var result = _context.Topics.Where(t=>t.CourseId==courseId);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetTopicsByCourseId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetTopicsByCourseId));
                throw;
            }
        }
        public Topic GetTopicById(int topicId)
        {
            if (topicId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetTopicById));
            try
            {
                return _context.Topics.Where(t=>t.Id==topicId).Include(t=>t.Attendances).Include(t=>t.Assignments).FirstOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetTopicById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetTopicById));
                throw;
            }
        }
        public Dictionary<string,string> CreateTopic(Topic topic)
        {
             if (topic == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateTopic), nameof(topic));
            var validation = Validation.ValidateTopic(topic,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    SetUpTopicDetails(topic);
                    CreateAndSaveTopic(topic);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(CreateTopic));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(CreateTopic));
                    throw;
                }
            }
            return validation;
        }

        private void CreateAndSaveTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        private void SetUpTopicDetails(Topic topic)
        {
            topic.isDisabled = false;
            topic.CreatedOn = DateTime.UtcNow;
        }

        public Dictionary<string,string> UpdateTopic(Topic topic)
        {
            if (topic == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateTopic), nameof(topic));
            var validation = Validation.ValidateTopic(topic,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbTopic = _context.Topics.Where(t=>t.Id==topic.Id).FirstOrDefault();
                    if (dbTopic != null)
                    {
                        SetUpTopicDetails(topic, dbTopic);
                        UpdateAndSaveTopic(dbTopic);
                    }
                    validation.Add("Invalid Id","Not Found");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(UpdateTopic));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(UpdateTopic));
                    throw;
                }
            }
            return validation;
        }

        private void UpdateAndSaveTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        private void SetUpTopicDetails(Topic topic, Topic dbTopic)
        {
            dbTopic.Name = topic.Name;
            dbTopic.Duration = topic.Duration;
            dbTopic.Content = topic.Content;
            dbTopic.UpdatedOn = DateTime.UtcNow;
        }

        public bool DisableTopic(int topicId)
        {
           if (topicId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableTopic));
            try
            {
                var dbTopic = _context.Topics.Find(topicId);
                if (dbTopic != null)
                {
                    dbTopic.isDisabled = true;
                    dbTopic.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveTopic(dbTopic);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(DisableTopic));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(DisableTopic));
                throw;
            }
        }        
    }
}