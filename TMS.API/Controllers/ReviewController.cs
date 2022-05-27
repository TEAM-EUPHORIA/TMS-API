
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

       [HttpGet("status/{statusId:int}")]
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