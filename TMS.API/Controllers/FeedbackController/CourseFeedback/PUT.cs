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
        /// Update a Feedback
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
        /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        [HttpPut("course/feedback")]
        [Authorize(Roles = "Trainee")]
        public IActionResult UpdateCourseFeedback([FromBody] CourseFeedback feedback)
        {
            if (feedback is null)
            {
                return BadRequest("feedback is required");
            }
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var feedbackExists = _service.Validation.CourseFeedbackExists(feedback.CourseId, feedback.TraineeId);
                var userId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                bool access = userId == feedback.TraineeId;
                if (access)
                {
                    if (feedbackExists)
                    {
                        var IsValid = _service.Validation.ValidateCourseFeedback(feedback);
                        if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                        {
                            int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                            var res = _service.FeedbackService.UpdateCourseFeedback(feedback, updatedBy);
                            if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                        }
                        return BadRequest(IsValid);
                    }
                    return NotFound("Not Found");
                }
                return Unauthorized("UnAuthorized, contect your admin");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}