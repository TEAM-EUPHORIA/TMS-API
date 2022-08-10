using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// Gets a list of Attendance
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/getAttendance
        ///
        /// </remarks>
        /// <response code="200">Returns a list of attendance present in a topic. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpGet("getAttendance/{courseId:int}/{topicId:int}")]
        [Authorize(Roles = "Trainer,Training Coordinator")]
        public IActionResult GetAttendanceList(int courseId, int topicId)
        {
            try
            {
                var courseExists = _service.Validation.CourseExists(courseId);
                var userId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                var isCoordinator = ControllerHelper.GetCurrentUserRole(this.HttpContext, _logger) == "Training Coordinator";
                bool access = isCoordinator || _service.Validation.ValidateCourseAccess(courseId, userId);
                if (courseExists && access)
                {
                    var result = _service.CourseService.GetAttendanceList(courseId, topicId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}