using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{

    public partial class CourseService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;

        public CourseService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId)
        {
            var topicExists = _repo.Validation.TopicExists(topicId);
            if (topicExists)
            {
                return _repo.Courses.GetAssignmentsByTopicId(topicId);
            }
            throw new ArgumentException("Invalid Id");
        }
        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            var assignmentExists = _repo.Validation.AssignmentExists(courseId, topicId, ownerId);
            if (assignmentExists)
            {
                return _repo.Courses.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId);
            }
            throw new ArgumentException("Invalid Id's");
        }
        public Dictionary<string, string> CreateAssignment(Assignment assignment, int createdBy)
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
        public Dictionary<string, string> UpdateAssignment(Assignment assignment, int updatedBy)
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
    }
}