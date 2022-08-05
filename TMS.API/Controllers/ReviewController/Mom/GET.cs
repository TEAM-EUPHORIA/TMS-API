using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers.ReviewController
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public partial class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IUnitOfService _service;
        public ReviewController(IUnitOfService service, ILogger<ReviewController> logger)
        {
            _logger = logger;
            _service = service;
        }
        /// <summary>
        /// Gets list of MOM by UserId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Review/mom/user/(userId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns the list of MOM</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If there is problem in server</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="userId"></param>
        [HttpGet("mom/user/{userId:int}")]
        public IActionResult GetListOfMomByUserId(int userId)
        {
            try
            {
                var userExists = _service.Validation.UserExists(userId);
                if (userExists)
                {
                    var result = _service.ReviewService.GetListOfMomByUserId(userId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewController), nameof(GetListOfMomByUserId));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(GetListOfMomByUserId));
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Get a MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Review/mom/(reviewId:int,traineeId:int)
        ///
        /// </remarks>
        /// <response code="200">If the course was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If MOM was not found.</response>
        /// <response code="500">Returns a MOM.</response>
        /// <param name="reviewId"></param>
        /// <param name="traineeId"></param>
        [HttpGet("mom/{reviewId:int},{traineeId:int}")]
        public IActionResult GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
        {
            try
            {
                var momExists = _service.Validation.MOMExists(reviewId, traineeId);
                if (momExists)
                {
                    var result = _service.ReviewService.GetMomByReviewIdAndTraineeId(reviewId, traineeId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(ReviewController), nameof(GetMomByReviewIdAndTraineeId));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(GetMomByReviewIdAndTraineeId));
                return Problem("sorry somthing went wrong");
            }
        }
    }
}