using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class FeedBackController : ControllerBase
    {
        /// <summary>
        /// Gets a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/trainee/(courseId:int),(traineeId:int),(trainerId:int)
        /// 
        ///
        /// </remarks>
        /// <response code="200">Returns a feedback</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <param name="trainerId"></param>
        [HttpGet("trainee/{courseId:int},{traineeId:int},{trainerId:int}")]
        public IActionResult GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId)
        {
            try
            {
                var feedbackExists = _service.Validation.TraineeFeedbackExists(courseId, traineeId, trainerId);
                if (feedbackExists)
                {
                    var result = _service.FeedbackService.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId, traineeId, trainerId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("NotFound");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}