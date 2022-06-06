
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
        ///        "statusId": 1  (1-Assigned; 2-Completed; 3-Cancelled)
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="statusId"></param>
       /// <returns></returns>
       [HttpGet("review/status/{statusId:int}")]
        public IActionResult GetReviewByStatusId(int statusId)
        {
            var statusExists = Validation.ReviewStatusExists(_context,statusId);
            if(statusExists)
            {
                try
                {
                    var result = _reviewService.GetReviewByStatusId(statusId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetReviewByStatusId));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
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
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="reviewId"></param>
       /// <returns></returns>
        [HttpGet("{reviewId:int}")]
        public IActionResult GetReviewById(int reviewId)
        {
            var reviewExists = Validation.ReviewExists(_context,reviewId);
            if(reviewExists)
            {
                try
                {
                    var result = _reviewService.GetReviewById(reviewId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetReviewById));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
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
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost("review")]
        public IActionResult CreateReview(Review review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateReview(review,_context);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create the review. the review already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _reviewService.CreateReview(review,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(CreateReview));
                return Problem(ProblemResponse);
            }
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
        [HttpPut("review")]
        public IActionResult UpdateReview(Review review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var reviewExists = Validation.ReviewExists(_context,review.Id);
            if(reviewExists)
            {
                try
                {
                    var IsValid = Validation.ValidateReview(review,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _reviewService.UpdateReview(review,_context);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Review was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(UpdateReview));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();

        }
        /// <summary>
        /// This method is invoked when the Coordinator/Reviewer wants to view the list of MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetListOfMomByUserId
        ///     {
        ///        "userId": 13      
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="userId"></param>
       /// <returns></returns>
        [HttpGet("mom/user/{userId:int}")]
        public IActionResult GetListOfMomByUserId(int userId)
        {
            var userExists = Validation.UserExists(_context,userId);
            if(userExists)
            {
                try
                {
                    var result = _reviewService.GetListOfMomByUserId(userId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetListOfMomByUserId));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
          /// <summary>
        /// This method is invoked when the Reviewer/Trainee/Coordinator wants to view the MOM
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetMomById
        ///     {
        ///        "reviewId": 1,
        ///        "traineeId":13   
        ///      }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="reviewId"></param>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        [HttpGet("mom/{reviewId:int},{traineeId:int}")]
        public IActionResult GetMomByReviewIdAndTraineeId(int reviewId,int traineeId)
        {
            var momExists = Validation.MOMExists(_context,reviewId,traineeId);
            if(momExists)
            {
                try
                {
                    var result = _reviewService.GetMomByReviewIdAndTraineeId(reviewId,traineeId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(GetMomByReviewIdAndTraineeId));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
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
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="mom"></param>
        /// <returns></returns>
        [HttpPost("mom")]
        public IActionResult CreateMom(MOM mom)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateMOM(mom,_context);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create the mom. The Mom Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _reviewService.CreateMom(mom,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(CreateMom));
                return Problem(ProblemResponse);
            }
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
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="mom"></param>
        /// <returns></returns>
        [HttpPut("mom")]
        public IActionResult UpdateMom(MOM mom)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var momExists = Validation.MOMExists(_context,mom.ReviewId,mom.TraineeId);
            if(momExists)
            {
                try
                {
                    var IsValid = Validation.ValidateMOM(mom,_context);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        var res = _reviewService.UpdateMom(mom,_context);
                        if (res.ContainsKey("Exists") && res.ContainsKey("IsValid")) return Ok(new { Response = "The MOM was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(ReviewController), nameof(UpdateMom));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }


    }
}