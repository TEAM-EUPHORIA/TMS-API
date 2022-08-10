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
        /// To Create a Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Review/review
        /// 
        ///      * fields are required
        /// 
        ///     POST /CreateReview
        ///     {
        ///        reviewerId* : int,
        ///        statusId* : int,
        ///        traineeId* : int,
        ///        reviewDate* : DateTime,
        ///        reviewTime* : DateTime,
        ///        mode* : String  
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Review was created..</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="review"></param>
        [HttpPost("review")]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateReview([FromBody] Review review)
        {
            if (review is null)
            {
                return BadRequest("review is required");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateReview(review);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create the review. the review already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                    var res = _service.ReviewService.CreateReview(review, createdBy);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}