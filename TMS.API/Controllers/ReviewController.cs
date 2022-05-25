
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