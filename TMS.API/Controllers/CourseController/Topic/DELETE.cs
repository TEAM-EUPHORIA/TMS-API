using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// To disable the topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId)/topics/disable/(topicId:int)
        ///
        /// </remarks>
        /// <response code="200">If the topic was disabled </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course or topic was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpPut("{courseId:int}/topics/disable/{topicId:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult DisableTopic(int courseId, int topicId)
        {
            var topicExists = _service.Validation.TopicExists(topicId, courseId);
            if (topicExists)
            {
                try
                {
                    // int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    int currentUserId = 2041;
                    var res = _service.CourseService.DisableTopic(courseId, topicId, currentUserId);
                    if (res) return Ok(new { message = "The User was Disabled successfully" });
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(DisableTopic));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("NotFound");
        }
    }
}