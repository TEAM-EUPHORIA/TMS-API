using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public partial class ReviewController : ControllerBase
    {
        /// <summary>
        /// Get all Review by Status 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Review/review/status/(statusId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a Review.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="statusId"></param>
        [HttpGet("review/status/{statusId:int}")]
        public IActionResult GetReviewByStatusId(int statusId)
        {
            var statusExists = _service.Validation.ReviewStatusExists(statusId);
            if (statusExists)
            {
                try
                {
                    var result = _service.ReviewService.GetReviewByStatusId(statusId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewController), nameof(GetReviewByStatusId));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }
        /// <summary>
        /// Get a Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Review/(reviewId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a Review.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="reviewId"></param>
        [HttpGet("{reviewId:int}")]
        public IActionResult GetReviewById(int reviewId)
        {
            var reviewExists = _service.Validation.ReviewExists(reviewId);
            if (reviewExists)
            {
                try
                {
                    var result = _service.ReviewService.GetReviewById(reviewId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewController), nameof(GetReviewById));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }
    }
}