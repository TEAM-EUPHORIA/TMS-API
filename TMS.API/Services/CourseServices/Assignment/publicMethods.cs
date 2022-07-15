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

        /// <summary>
        /// Constructor of CourseService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>

        public CourseService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        /// used to get Assignment based on topic id
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns>
        /// IEnumerable Assignment if Topic is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId)
        {
            try
            {
            var topicExists = _repo.Validation.TopicExists(topicId);
            if (topicExists)
            {
                return _repo.Courses.GetAssignmentsByTopicId(topicId);
            }
            else throw new ArgumentException("Invalid Id");
            }
            
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetAssignmentsByTopicId));
                throw;
            }
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
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            try
            {
            var assignmentExists = _repo.Validation.AssignmentExists(courseId, topicId, ownerId);
            if (assignmentExists)
            {
                return _repo.Courses.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId);
            }
            else throw new ArgumentException("Invalid Id's");
            }
            
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(GetAssignmentByCourseIdTopicIdAndOwnerId));
                throw;
            }

        }
        
        /// <summary>
        /// used to create assignment.
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Dictionary<string, string> CreateAssignment(Assignment assignment, int createdBy)
        {
            try
            {

            if (assignment is null) throw new ArgumentNullException(nameof(assignment));
            var validation = _repo.Validation.ValidateAssignment(assignment);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                PrepareAssignment(assignment,createdBy);
                _repo.Courses.CreateAssignment(assignment);
                _repo.Complete();
            }
            return validation;
            }
            
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(CreateAssignment));
                throw;
            }
        }

        /// <summary>
        /// used to Update assignment.
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// validation Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> UpdateAssignment(Assignment assignment, int updatedBy)
        {
            try
            {
            if (assignment is null) throw new ArgumentNullException(nameof(assignment));
            var validation = _repo.Validation.ValidateAssignment(assignment);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbAssignment = _repo.Courses.GetAssignmentByCourseIdTopicIdAndOwnerId(assignment.CourseId, assignment.TopicId, assignment.OwnerId);
                PrepareAssignment(assignment, dbAssignment,updatedBy);
                _repo.Courses.UpdateAssignment(dbAssignment);
                _repo.Complete();
            }
            return validation;
            }
            
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseService), nameof(UpdateAssignment));
                throw;
            }

        }
    }
}