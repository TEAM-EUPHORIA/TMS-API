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
        private readonly IStatistics _stats;
        public FeedBackController(IUnitOfService service, ILogger<FeedBackController> logger, IStatistics stats)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _service = service ?? throw new ArgumentException(nameof(service));
            _stats = stats ?? throw new ArgumentException(nameof(stats));
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
        [Authorize(Roles = "Training Head, Training Coordinator, Trainee")]
        public IActionResult GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId)
        {
            try
            {
                var feedbackExists = _service.Validation.CourseFeedbackExists(courseId, traineeId);
                if (feedbackExists)
                {
                    var result = _service.FeedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId, traineeId);
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
    }
}