using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// To disable the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/disable/(courseId:int)
        ///
        /// </remarks>
        /// <response code="200">If the course was disabled </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        [HttpDelete("disable/{courseId:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult DisableCourse(int courseId)
        {
            var courseExists = _service.Validation.CourseExists(courseId);
            if (courseExists)
            {
                try
                {
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.CourseService.DisableCourse(courseId, currentUserId);
                    if (res) return Ok(new { message = "The User was Disabled successfully" });
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(DisableCourse));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("NotFound");
        }
    }
}