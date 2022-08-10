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
        /// To create a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/FeedBack/trainee/feedback
        /// 
        ///     * fields are required
        ///
        ///     POST /CreateTraineeFeedback
        ///     {
        ///       courseId* : int,
        ///       traineeId* : int,
        ///       trainerId* : int,
        ///       feedback* : string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was submited. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        [HttpPost("trainee/feedback")]
        [Authorize(Roles = "Trainer")]
        public IActionResult CreateTraineeFeedback([FromBody] TraineeFeedback feedback)
        {
            if (feedback is null)
            {
                return BadRequest("feedback is required");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateTraineeFeedback(feedback);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create feedback. the feedback Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                    var res = _service.FeedbackService.CreateTraineeFeedback(feedback, createdBy);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}