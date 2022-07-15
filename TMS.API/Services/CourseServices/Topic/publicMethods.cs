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
            var courseExists = _repo.Validation.CourseExists(courseId);
            if (courseExists) return _repo.Courses.GetTopicsByCourseId(courseId);
            else throw new ArgumentException("Invalid Id");
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
            var topicExists = _repo.Validation.TopicExists(topicId);
            if (topicExists)
            {
                return _repo.Courses.GetTopicById(courseId, topicId, userId);
            }
            else throw new ArgumentException("Invalid Id");
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
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = _repo.Validation.ValidateTopic(topic);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpTopicDetails(topic,createdBy);
                _repo.Courses.CreateTopic(topic);
                _repo.Complete();
            }
            return validation;
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
            if (topic is null) throw new ArgumentNullException(nameof(topic));
            var validation = _repo.Validation.ValidateTopic(topic);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbTopic = _repo.Courses.GetTopicById(topic.CourseId, topic.TopicId);
                SetUpTopicDetails(topic, dbTopic,updatedBy);
                _repo.Courses.UpdateTopic(dbTopic);
                _repo.Complete();
            }
            return validation;
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
            var topicExists = _repo.Validation.TopicExists(topicId, courseId);
            if (topicExists)
            {
                var dbTopic = _repo.Courses.GetTopicById(courseId, topicId);
                Disable(updatedBy, dbTopic);
                _repo.Complete();
            }
            return topicExists;
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
            var validation = _repo.Validation.ValidateAttendance(attendance);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                _repo.Courses.MarkAttendance(attendance);
                _repo.Complete();
            }
            return validation;
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
        public IEnumerable<Attendance> GetAttendanceList(int courseId,int topicId){
            var courseExists = _repo.Validation.CourseExists(courseId);
            var topicExists = _repo.Validation.TopicExists(topicId);
            if(courseExists && topicExists) return _repo.Courses.GetAttendanceList(courseId,topicId);
            else throw new ArgumentException("Invalid Id");
        }
    }
}