
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ReviewService _reviewService;
        private readonly Validation _validation;

        public ReviewController(UnitOfService service,ILogger<ReviewController> logger)
        {
            _logger = logger;
            _reviewService = service.ReviewService;
            _validation = service.Validation;
        }

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
            var statusExists = _validation.ReviewStatusExists(statusId);
            if(statusExists)
            {
                try
                {
                    var result = _reviewService.GetReviewByStatusId(statusId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetReviewByStatusId));
                    return Problem();
                }
            }
            return NotFound();
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
            var reviewExists = _validation.ReviewExists(reviewId);
            if(reviewExists)
            {
                try
                {
                    var result = _reviewService.GetReviewById(reviewId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetReviewById));
                    return Problem();
                }
            }
            return NotFound();
        }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateReview(Review review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _validation.ValidateReview(review);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create the review. the review already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    review.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _reviewService.CreateReview(review);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(CreateReview));
                return Problem();
            }
        }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateReview(Review review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var reviewExists = _validation.ReviewExists(review.Id);
            if(reviewExists)
            {
                try
                {
                    var IsValid = _validation.ValidateReview(review);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        review.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _reviewService.UpdateReview(review);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(UpdateReview));
                    return Problem();
                }
            }
            return NotFound();

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
            var userExists = _validation.UserExists(userId);
            if(userExists)
            {
                try
                {
                    var result = _reviewService.GetListOfMomByUserId(userId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetListOfMomByUserId));
                    return Problem();
                }
            }
            return NotFound();
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
        public IActionResult GetMomByReviewIdAndTraineeId(int reviewId,int traineeId)
        {
            var momExists = _validation.MOMExists(reviewId,traineeId);
            if(momExists)
            {
                try
                {
                    var result = _reviewService.GetMomByReviewIdAndTraineeId(reviewId,traineeId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetMomByReviewIdAndTraineeId));
                    return Problem();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Create a MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Review/mom
        ///
        ///     * fields are required
        ///     body
        ///     {
        ///       reviewId* : int
        ///       statusId* : int
        ///       ownerId* : int
        ///       agenda* : string
        ///       meetingNotes* : string
        ///       purposeOfMeeting* : string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the MOM was created.</response>
        /// <response code="404">If MOM was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="mom"></param>
        [HttpPost("mom")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainee")]
        public IActionResult CreateMom(MOM mom)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _validation.ValidateMOM(mom);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create the mom. The Mom Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    mom.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _reviewService.CreateMom(mom);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(CreateMom));
                return Problem();
            }
        }

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
            var momExists = _validation.MOMExists(mom.ReviewId,mom.TraineeId);
            if(momExists)
            {
                try
                {
                    var IsValid = _validation.ValidateMOM(mom);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        mom.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _reviewService.UpdateMom(mom);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(UpdateMom));
                    return Problem();
                }
            }
            return NotFound();
        }
    }
}