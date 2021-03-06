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
        /// To Update Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Review/review
        /// 
        ///     * fields are required
        /// 
        ///     PUT /UpdateReview
        ///     {
        ///        id* : int,
        ///        reviewerId* : int,
        ///        statusId* : int,
        ///        traineeId* : int,
        ///        reviewDate* : DateTime,
        ///        reviewTime* : DateTime,
        ///        mode* : string
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Review was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="review"></param>
        [HttpPut("review")]
        
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateReview([FromBody]Review review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var reviewExists = _service.Validation.ReviewExists(review.Id);
            if (reviewExists)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateReview(review);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.ReviewService.UpdateReview(review, updatedBy);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewController), nameof(UpdateReview));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }
    }
}