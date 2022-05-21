
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedBackController : MyBaseController
    {
        private readonly ILogger<FeedBackController> _logger;
        private readonly FeedbackService _feedbackService;

        public FeedBackController(ILogger<FeedBackController> logger, FeedbackService feedbackService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _feedbackService = feedbackService;
        }

       [HttpGet("/Feedback/Course/{courseId:int},{ownwerId:int}")]
        public IActionResult GetCourseFeedbackByCourseIdAndOwnerId(int courseId,int ownwerId)
        {
            if (courseId == 0||ownwerId==0) BadId();
            try
            {
                var result = _feedbackService.GetCourseFeedbackByCourseIdAndOwnerId(courseId,ownwerId);
                if (result != null) return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(GetCourseFeedbackByCourseIdAndOwnerId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(GetCourseFeedbackByCourseIdAndOwnerId));
            }
            return Problem(ProblemResponse);
        }
        [HttpPost("/Feedback/Course/")]
        public IActionResult CreateCourseFeedback(CourseFeedback feedback)
        {
            if (feedback == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateCourseFeedback(feedback,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _feedbackService.CreateCourseFeedback(feedback);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateCourseFeedback));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(CreateCourseFeedback));
            }
            return Problem(ProblemResponse);
        }

        [HttpPut("/Feedback/Course/")]
        public IActionResult UpdateCourseFeedback(CourseFeedback feedback)
        {
            if (feedback == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateCourseFeedback(feedback,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _feedbackService.UpdateCourseFeedback(feedback);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(UpdateCourseFeedback));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(UpdateCourseFeedback));
            }
            return Problem(ProblemResponse);

        }
       [HttpGet("/Feedback/Trainee/{traineeId:int},{trainerId:int}")]
        public IActionResult GetTraineeFeedbackByTrainerIdAndTraineeId(int traineeId,int trainerId)
        {
            if (traineeId == 0||trainerId==0) BadId();
            try
            {
                var result = _feedbackService.GetTraineeFeedbackByTrainerIdAndTraineeId(traineeId,trainerId);
                if (result != null) return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(GetTraineeFeedbackByTrainerIdAndTraineeId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(GetTraineeFeedbackByTrainerIdAndTraineeId));
            }
            return Problem(ProblemResponse);
        }
        [HttpPost("/Feedback/Trainee/")]
        public IActionResult CreateTraineeFeedback(TraineeFeedback feedback)
        {
            if (feedback == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateTraineeFeedback(feedback,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _feedbackService.CreateTraineeFeedback(feedback);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateTraineeFeedback));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(CreateTraineeFeedback));
            }
            return Problem(ProblemResponse);
        }

        [HttpPut("/Feedback/Trainee/")]
        public IActionResult UpdateTraineeFeedback(TraineeFeedback feedback)
        {
            if (feedback == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateTraineeFeedback(feedback,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _feedbackService.UpdateTraineeFeedback(feedback);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(UpdateCourseFeedback));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(UpdateCourseFeedback));
            }
            return Problem(ProblemResponse);

        }
    }
}