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
        /// <summary>
        /// Constructor for CourseController
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public CourseController(IUnitOfService service, ILogger<CourseController> logger)
        {
            _logger = logger;
            _service = service;
        }
        /// <summary>
        /// Gets a list of assigments in a topic by courseId and topicId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)/assignments
        ///
        /// </remarks>
        /// <response code="200">Returns a list of assigments present in a topic. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments")]
        public IActionResult GetAssignmentsByTopicId(int courseId, int topicId)
        {
            var topicExists = _service.Validation.TopicExists(topicId, courseId);
            if (topicExists)
            {
                try
                {
                    return Ok(_service.CourseService.GetAssignmentsByTopicId(topicId));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(GetAssignmentsByTopicId));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not found");
        }
        /// <summary>
        /// Gets a single assigments in a topic by courseId, topicId and ownerId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)/assignments/(ownerId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single assigments in a topic by courseId, topicId and ownerId. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If assignment was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments/{ownerId:int}")]
        public IActionResult GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            var assignmentExists = _service.Validation.AssignmentExists(courseId, topicId, ownerId);
            if (assignmentExists)
            {
                try
                {
                    var result = _service.CourseService.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(GetAssignmentByCourseIdTopicIdAndOwnerId));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }
    }
}