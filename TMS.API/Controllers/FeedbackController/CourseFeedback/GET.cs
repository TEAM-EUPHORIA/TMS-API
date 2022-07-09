using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public partial class FeedBackController : ControllerBase
    {
        private readonly ILogger<FeedBackController> _logger;
        private readonly IUnitOfService _service;
        public FeedBackController(IUnitOfService service, ILogger<FeedBackController> logger)
        {
            _logger = logger;
            _service = service;
        }
        /// <summary>
        /// Gets a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/course/(courseId:int),(traineeId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single Feedback.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If Feedback was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        [HttpGet("course/{courseId:int},{traineeId:int}")]
        public IActionResult GetCourseFeedbackByCourseIdAndTraineeId(int courseId,int traineeId)
        {
            var feedbackExists = _service.Validation.CourseFeedbackExists(courseId,traineeId);
            if(feedbackExists)
            {
                try
                {
                    var result = _service.FeedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedBackController), nameof(GetCourseFeedbackByCourseIdAndTraineeId));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("NotFound");
        }
    }
}