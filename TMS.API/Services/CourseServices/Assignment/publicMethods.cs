using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class CourseService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;
        const string INVALID_ID = "Invalid Id";
        /// <summary>
        /// Constructor of CourseService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public CourseService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        /// <summary>
        /// used to get Assignment based on topic id
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns>
        /// IEnumerable Assignment if Topic is found
        /// </returns>
        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId)
        {
            var topicExists = _repo.Validation.TopicExists(topicId);
            if (topicExists)
            {
                return _repo.Courses.GetAssignmentsByTopicId(topicId);
            }
            else throw new ArgumentException(INVALID_ID);
        }
        /// <summary>
        /// used to get assignment by courseId,topicId,ownerId
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        /// <returns>
        /// Assignment if Assignment is found
        /// </returns>
        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            var assignmentExists = _repo.Validation.AssignmentExists(courseId, topicId, ownerId);
            if (assignmentExists)
            {
                return _repo.Courses.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId);
            }
            else throw new ArgumentException(INVALID_ID);
        }
        /// <summary>
        /// used to create assignment.
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> CreateAssignment(Assignment assignment, int createdBy)
        {

            if (assignment is null) throw new ArgumentException(nameof(assignment));
            var validation = _repo.Validation.ValidateAssignment(assignment);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                PrepareAssignment(assignment, createdBy);
                _repo.Courses.CreateAssignment(assignment);
                _repo.Complete();
            }
            return validation;
        }
        /// <summary>
        /// used to Update assignment.
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> UpdateAssignment(Assignment assignment, int updatedBy)
        {
            if (assignment is null) throw new ArgumentException(nameof(assignment));
            var validation = _repo.Validation.ValidateAssignment(assignment);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbAssignment = _repo.Courses.GetAssignmentByCourseIdTopicIdAndOwnerId(assignment.CourseId, assignment.TopicId, assignment.OwnerId);
                PrepareAssignment(assignment, dbAssignment, updatedBy);
                _repo.Courses.UpdateAssignment(dbAssignment);
                _repo.Complete();
            }
            return validation;
        }
    }
}