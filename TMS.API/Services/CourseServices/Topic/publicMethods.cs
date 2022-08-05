using TMS.API.UtilityFunctions;
using TMS.BAL;


namespace TMS.API.Services
{
    public partial class CourseService : ICourseService
    {
        /// <summary>
        /// Fetching Topics details by CourseId.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>
        /// enumerable topic if course is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            try
            {
                var courseExists = _repo.Validation.CourseExists(courseId);
                if (courseExists) return _repo.Courses.GetTopicsByCourseId(courseId);
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetTopicsByCourseId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetTopicsByCourseId));
                throw;
            }
        }
        /// <summary>
        /// Fetching Topics details by CourseId,topicId,userId.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// topic details if topic is found
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Topic GetTopicById(int courseId, int topicId, int userId)
        {
            try
            {
                var topicExists = _repo.Validation.TopicExists(topicId);
                if (topicExists)
                {
                    return _repo.Courses.GetTopicById(courseId, topicId, userId);
                }
                else throw new ArgumentException(INVALID_ID);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetTopicById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetTopicById));
                throw;
            }
        }
        /// <summary>
        /// Create the topic
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation dictionary
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> CreateTopic(Topic topic, int createdBy)
        {
            try
            {
                if (topic is null) throw new ArgumentNullException(nameof(topic));
                var validation = _repo.Validation.ValidateTopic(topic);
                if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
                {
                    SetUpTopicDetails(topic, createdBy);
                    _repo.Courses.CreateTopic(topic);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(CreateTopic));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CreateTopic));
                throw;
            }
        }
        /// <summary>
        /// update the topic details by topicId
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation dictionary
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> UpdateTopic(Topic topic, int updatedBy)
        {
            try
            {
                if (topic is null) throw new ArgumentNullException(nameof(topic));
                var validation = _repo.Validation.ValidateTopic(topic);
                if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
                {
                    var dbTopic = _repo.Courses.GetTopicById(topic.CourseId, topic.TopicId);
                    SetUpTopicDetails(topic, dbTopic, updatedBy);
                    _repo.Courses.UpdateTopic(dbTopic);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(UpdateTopic));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UpdateTopic));
                throw;
            }
        }
        public Dictionary<string, string> UpdateTopic(Topic topic)
        {
            try
            {
                if (topic is null) throw new ArgumentNullException(nameof(topic));
                var validation = _repo.Validation.ValidateTopic(topic);
                if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
                {
                    _repo.Courses.UpdateTopic(topic);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(UpdateTopic));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UpdateTopic));
                throw;
            }
        }
        /// <summary>
        /// disable the topic by topicId
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="updatedBy"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// true if topic is exist
        /// </returns>
        public bool DisableTopic(int courseId, int topicId, int updatedBy)
        {
            try
            {
                var topicExists = _repo.Validation.TopicExists(topicId, courseId);
                if (topicExists)
                {
                    var dbTopic = _repo.Courses.GetTopicById(courseId, topicId);
                    Disable(updatedBy, dbTopic);
                    _repo.Complete();
                }
                return topicExists;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(DisableTopic));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DisableTopic));
                throw;
            }
        }
        /// <summary>
        /// mark the attendance 
        /// </summary>
        /// <param name="attendance"></param>
        /// <returns>
        /// validation dictionary
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> MarkAttendance(Attendance attendance)
        {
            try
            {
                var validation = _repo.Validation.ValidateAttendance(attendance);
                if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
                {
                    attendance.Status = true;
                    _repo.Courses.MarkAttendance(attendance);
                    _repo.Complete();
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(MarkAttendance));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(MarkAttendance));
                throw;
            }
        }
        /// <summary>
        /// get the attendance list
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// Enumerable attendance list if course and topic list exists 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Attendance> GetAttendanceList(int courseId, int topicId)
        {
            try
            {
                var courseExists = _repo.Validation.CourseExists(courseId);
                var topicExists = _repo.Validation.TopicExists(topicId);
                if (courseExists && topicExists) return _repo.Courses.GetAttendanceList(courseId, topicId);
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetAttendanceList));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetAttendanceList));
                throw;
            }
        }
    }
}