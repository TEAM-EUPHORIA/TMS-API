using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
using TMS.API.Services;
namespace TMS.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public partial class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly IUnitOfService _service;
        private readonly AppDbContext _context;
        /// <summary>
        /// Constructor for CourseController
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public CourseController(IUnitOfService service, ILogger<CourseController> logger, AppDbContext context)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _service = service ?? throw new ArgumentException(nameof(service));
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        /// <summary>
        /// Gets a list of assignments in a topic by courseId and topicId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)/assignments
        ///
        /// </remarks>
        /// <response code="200">Returns a list of assignments present in a topic. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments")]
        [Authorize(Roles = "Trainer,Trainee,Training Coordinator")]
        public IActionResult GetAssignmentsByTopicId(int courseId, int topicId)
        {
            var topicExists = _service.Validation.TopicExists(topicId, courseId);
            GetUserId(out int userId);
            CheckIsCoOrdinator(out bool isCoordinator);
            try
            {
                bool access = isCoordinator || _service.Validation.ValidateCourseAccess(courseId, userId);
                if (topicExists && access)
                {
                    return Ok(_service.CourseService.GetAssignmentsByTopicId(topicId));
                }
                return NotFound("Not found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets a single assignments in a topic by courseId, topicId and ownerId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)/assignments/(ownerId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single assignments in a topic by courseId, topicId and ownerId. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If assignment was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments/{ownerId:int}")]
        [Authorize(Roles = "Trainer,Trainee,Training Coordinator")]
        public IActionResult GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            var assignmentExists = _service.Validation.AssignmentExists(courseId, topicId, ownerId);
            GetUserId(out int userId);
            CheckIsCoOrdinator(out bool isCoordinator);
            try
            {
                bool access = isCoordinator || _service.Validation.ValidateCourseAccess(courseId, userId);
                if (assignmentExists && access)
                {
                    var result = _service.CourseService.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }

        private void GetUserId(out int userId)
        {
            userId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
        }
        private void CheckIsCoOrdinator(out bool isCoordinator)
        {
            isCoordinator = ControllerHelper.GetCurrentUserRole(this.HttpContext, _logger) == "Training Coordinator";
        }
    }
}