using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Controllers.ReviewController
{
    [Authorize]
    public partial class ReviewController : ControllerBase
    {
        /// <summary>
        /// Update a MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Review/mom
        ///
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///       id : int
        ///       reviewId* : int
        ///       statusId* : int
        ///       ownerId* : int
        ///       agenda* : string
        ///       meetingNotes* : string
        ///       purposeOfMeeting* : string  
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the MOM was updated.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If MOM was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="mom"></param>
        [HttpPut("mom")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainee")]
        public IActionResult UpdateMom(MOM mom)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var momExists = _service.Validation.MOMExists(mom.ReviewId, mom.TraineeId);
            if (momExists)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateMOM(mom);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        mom.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.ReviewService.UpdateMom(mom);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewController), nameof(UpdateMom));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }
    }
}