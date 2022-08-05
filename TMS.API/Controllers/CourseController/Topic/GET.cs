using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// Gets a list of topics in a course by courseId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics
        ///
        /// </remarks>
        /// <response code="200">Returns a list of topics present in a course </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        [HttpGet("{courseId:int}/topics")]
        [Authorize(Roles = "Training Head, Training Coordinator, Trainer, Trainee")]
        public IActionResult GetTopicsByCourseId(int courseId)
        {
            try
            {
                var courseExists = _service.Validation.CourseExists(courseId);
                var userId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                bool access = false;
                var check = ControllerHelper.GetCurrentUserRole(this.HttpContext) == "Training Coordinator";
                if (check)
                {
                    access = true;
                }
                else
                {
                    access = _service.Validation.ValidateCourseAccess(courseId, userId);
                }
                if (courseExists && access)
                {
                    return Ok(_service.CourseService.GetTopicsByCourseId(courseId));
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(GetTopicsByCourseId));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetTopicsByCourseId));
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets a single topic by courseId and topicId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a list of topics present in a course </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}")]
        [Authorize(Roles = "Training Head, Training Coordinator, Trainer, Trainee")]
        public IActionResult GetTopicByIds(int courseId, int topicId)
        {
            try
            {
                var topicExists = _service.Validation.TopicExists(topicId, courseId);
                var userId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                bool access = false;
                var check = ControllerHelper.GetCurrentUserRole(this.HttpContext) == "Training Coordinator";
                if (check)
                {
                    access = true;
                }
                else
                {
                    access = _service.Validation.ValidateCourseAccess(courseId, userId);
                }
                if (topicExists && access)
                {
                    var result = _service.CourseService.GetTopicById(courseId, topicId, userId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(GetTopicByIds));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(GetTopicByIds));
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets a list of users present in a course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/getCourseUser/(courseId:int)
        ///
        /// </remarks>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("getCourseUser/{courseId:int}")]
        [Authorize(Roles = "Training Head, Training Coordinator, Trainer")]
        public IActionResult GetCourseUser(int courseId)
        {
            try
            {    
                var courseExists = _service.Validation.CourseExists(courseId);
                if (courseExists)
                {
                    var result = _service.CourseService.GetCourseUsers(courseId);
                    return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(GetCourseUser));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(GetCourseUser));
                return Problem("sorry somthing went wrong");
            }
        }
    }
}