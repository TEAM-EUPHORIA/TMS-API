using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [Authorize]
    public partial class FeedBackController : ControllerBase
    {
        /// <summary>
        ///  Create a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/course/feedback
        /// 
        ///     * fields are required
        /// 
        ///     {
        ///        courseId* : int,
        ///        traineeId* : int,
        ///        feedback* : string,
        ///        rating* : float
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="feedback"></param>
        [HttpPost("course/feedback")]
        [Authorize(Roles = "Trainee")]
        public IActionResult CreateCourseFeedback([FromBody] CourseFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = ControllerHelper.GetCurrentUserId(this.HttpContext);
            bool access = _service.Validation.ValidateCourseAccess(feedback.CourseId, userId);
            bool courseComplete = _stats.IsCourseCompleted(feedback.CourseId);
            if (access && courseComplete)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateCourseFeedback(feedback);
                    if (IsValid.ContainsKey("Exists")) return BadRequest("Can't submit the feedback. The feedback Already exists");
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.FeedbackService.CreateCourseFeedback(feedback, createdBy);
                        if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(FeedBackController), nameof(CreateCourseFeedback));
                    return Problem("sorry somthing went wrong");
                }
            }
            else if (!access)
            {
                return Unauthorized("UnAuthorized, contect your admin");
            }
            else
            {
                return BadRequest("The course is not yet completed");
            }
        }
    }
}