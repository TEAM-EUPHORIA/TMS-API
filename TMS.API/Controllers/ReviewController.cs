
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : MyBaseController
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ReviewService _reviewService;

        public ReviewController(ILogger<ReviewController> logger, ReviewService reviewService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _reviewService = reviewService;
        }
       /// <summary>
        /// This method is invoked when the Coorinator wants to view the Reviews based on the status 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetReviewByStatusId
        ///     {
        ///        "statusId": 1  (1-Completed; 2-Cancelled; 3-Assigned)
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="statusId"></param>
       /// <returns></returns>
       [HttpGet("/Review/Status/{statusId:int}")]
        public IActionResult GetReviewByStatusId(int statusId)
        {
            if (statusId == 0) BadId();
            try
            {
                var result = _reviewService.GetReviewByStatusId(statusId);
                if (result != null) return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetReviewByStatusId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetReviewByStatusId));
            }
            return Problem(ProblemResponse);
        }
        /// <summary>
        /// This method is invoked when the Coordinator/Trainee/Reviewer wants to view the Assigned Reviews
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetReviewById
        ///     {
        ///        "reviewId": 4
        ///   
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="reviewId"></param>
       /// <returns></returns>
        [HttpGet("/Review/{reviewId:int}")]
        public IActionResult GetReviewById(int reviewId)
        {
            if (reviewId == 0) return BadId();
            try
            {
                var result = _reviewService.GetReviewById(reviewId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetReviewById));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetReviewById));
            }
            return Problem(ProblemResponse);
        }
        /// <summary>
        /// This method is invoked when the Coordinator wants to Create a Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateReview
        ///     {
        ///        "reviewerId": 11,
        ///        "statusId": 1,
        ///        "traineeId": 15,
        ///        "reviewDate": "20-07-2022",
        ///        "reviewTime": "10:40AM",
        ///        "mode": "online",
        ///        "isDisabled": "false"   
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost("/Review")]
        public IActionResult CreateReview(Review review)
        {
            if (review == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateReview(review,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _reviewService.CreateReview(review);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(CreateReview));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(CreateReview));
            }
            return Problem(ProblemResponse);
        }

        /// <summary>
        /// This method is invoked when the Coordinator wants to Update Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateReview
        ///     {
        ///        "id": 1,
        ///        "reviewerId": 11,
        ///        "statusId": 1,
        ///        "traineeId": 15,
        ///        "reviewDate": "20-07-2022",
        ///        "reviewTime": "10:00AM",
        ///        "mode": "offline",
        ///        "isDisabled": "false"
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="review"></param>
       /// <returns></returns>
        [HttpPut("/Update")]
        public IActionResult UpdateReview(Review review)
        {
            if (review == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateReview(review,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _reviewService.UpdateReview(review);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(UpdateReview));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(UpdateReview));
            }
            return Problem(ProblemResponse);

        }
        /// <summary>
        /// This method is invoked when the Coordinator wants to Disable Review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /DisableReview
        ///     {
        ///        "reviewId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="reviewId"></param>
       /// <returns></returns>
        [HttpPut("/Review/Disable/{reviewId:int}")]
        public IActionResult DisableReview(int reviewId)
        {
            if (reviewId == 0) return BadId();
            try
            {
                var res = _reviewService.DisableReview(reviewId);
                if (res) return Ok("The Review was Disabled successfully");
                return BadId();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(DisableReview));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(DisableReview));
            }
            return Problem(ProblemResponse);

        }
        /// <summary>
        /// This method is invoked when the Coordinator/Reviewer wants to view the list of MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetListOfMomByUserId
        ///     {
        ///        "userId": 8      
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="userId"></param>
       /// <returns></returns>
        [HttpGet("MOM/User/{userId:int}")]
        public IActionResult GetListOfMomByUserId(int userId)
        {
            if (userId == 0) BadId();
            try
            {
                var result = _reviewService.GetListOfMomByUserId(userId);
                if (result != null) return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetListOfMomByUserId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetListOfMomByUserId));
            }
            return Problem(ProblemResponse);
        }
          /// <summary>
        /// This method is invoked when the Reviewer/Trainee/Coordinator wants to view the MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetMomById
        ///     {
        ///        "momId": 1    
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="momId"></param>
        /// <returns></returns>
        [HttpGet("MOM/{momId:int}")]
        public IActionResult GetMomById(int momId)
        {
            if (momId == 0) return BadId();
            try
            {
                var result = _reviewService.GetMomById(momId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetMomById));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetMomById));
            }
            return Problem(ProblemResponse);
        }
       /// <summary>
        /// This method is invoked when the trainee wants to create MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateMom
        ///     {
       ///       "reviewId": 1,
        ///       "statusId": 1,
        ///       "ownerId": 13,
        ///       "agenda": "Type Meeting Agenda...",
        ///       "meetingNotes": "Type Meeting Notes...",
        ///       "purposeOfMeeting": "Type purposeofMetting ..."     
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="mom"></param>
        /// <returns></returns>
        [HttpPost("MOM")]
        public IActionResult CreateMom(MOM mom)
        {
            if (mom == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateMOM(mom,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _reviewService.CreateMom(mom);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(CreateMom));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(CreateMom));
            }
            return Problem(ProblemResponse);
        }
          /// <summary>
        /// This method is invoked when the trainee wants to update MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateMom
        ///     {
        ///       "id": 1,
        ///       "reviewId": 1,
        ///       "statusId": 1,
        ///       "ownerId": 13,
        ///       "agenda": "Modify the Agenda",
        ///       "meetingNotes": "Modify the Meeting Notes",
        ///       "purposeOfMeeting": "Modify the purpose of meeting"
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="mom"></param>
        /// <returns></returns>
        [HttpPut("MOM")]
        public IActionResult UpdateMom(MOM mom)
        {
            if (mom == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateMOM(mom,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _reviewService.UpdateMom(mom);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(UpdateMom));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(UpdateMom));
            }
            return Problem(ProblemResponse);

        }


    }
}