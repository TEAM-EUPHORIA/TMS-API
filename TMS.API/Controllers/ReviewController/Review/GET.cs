using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers.ReviewController
{
    [Authorize]
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
            try
            {
                var statusExists = _service.Validation.ReviewStatusExists(statusId);
                if (statusExists)
                {
                    var result = _service.ReviewService.GetReviewByStatusId(statusId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Get all Review by Status 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Review/review/status/(statusId:int),(userId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a Review.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="statusId"></param>
        /// <param name="userId"></param>
        [HttpGet("review/status/{statusId:int},{userId:int}")]
        public IActionResult GetReviewByStatusId(int statusId, int userId)
        {
            try
            {
                var statusExists = _service.Validation.ReviewStatusExists(statusId);
                if (statusExists)
                {
                    var result = _service.ReviewService.GetReviewByStatusId(statusId, userId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
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
            try
            {
                var reviewExists = _service.Validation.ReviewExists(reviewId);
                if (reviewExists)
                {
                    var result = _service.ReviewService.GetReviewById(reviewId);
                    if (result is not null) return Ok(result);
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