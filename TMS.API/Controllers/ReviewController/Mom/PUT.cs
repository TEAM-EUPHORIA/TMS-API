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
        [Authorize(Roles = "Trainee")]
        public IActionResult UpdateMom([FromBody] MOM mom)
        {
            if (mom is null)
            {
                return BadRequest("Mom is required");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var momExists = _service.Validation.MOMExists(mom.ReviewId, mom.TraineeId);
                if (momExists)
                {
                    var IsValid = _service.Validation.ValidateMOM(mom);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                        var res = _service.ReviewService.UpdateMom(mom, updatedBy);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Updated successfully" });
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