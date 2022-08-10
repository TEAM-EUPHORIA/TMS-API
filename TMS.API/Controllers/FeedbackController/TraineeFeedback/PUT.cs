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
        /// To update a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/trainee/feedback
        /// 
        ///      * fields are required
        /// 
        ///     PUT /UpdateTraineeFeedback
        ///     {
        ///        courseId* : int,
        ///        traineeId* : int,
        ///        trainerId* : int,
        ///        feedback* : string  
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        [HttpPut("trainee/feedback")]
        [Authorize(Roles = "Trainer")]
        public IActionResult UpdateTraineeFeedback([FromBody] TraineeFeedback feedback)
        {
            if (feedback is null)
            {
                return BadRequest("feedback is required");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var feedbackExists = _service.Validation.TraineeFeedbackExists(feedback.CourseId, feedback.TraineeId, feedback.TrainerId);
                if (feedbackExists)
                {
                    var IsValid = _service.Validation.ValidateTraineeFeedback(feedback);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                        var res = _service.FeedbackService.UpdateTraineeFeedback(feedback, updatedBy);
                        if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    }
                    return BadRequest(IsValid);
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