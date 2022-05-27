
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

       [HttpGet("course/{courseId:int},{traineeId:int}")]
        public IActionResult GetCourseFeedbackByCourseIdAndTraineeId(int courseId,int traineeId)
        {
            var feedbackExists = Validation.CourseFeedbackExists(_context,courseId,traineeId);
            if(feedbackExists)
            {
                try
                {
                    var result = _feedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(GetCourseFeedbackByCourseIdAndTraineeId));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
        [HttpPost("course")]
        public IActionResult CreateCourseFeedback(CourseFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateCourseFeedback(feedback,_context);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't submit the feedback. The feedback Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _feedbackService.CreateCourseFeedback(feedback,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateCourseFeedback));
                return Problem(ProblemResponse);
            }
        }

        [HttpPut("course")]
        public IActionResult UpdateCourseFeedback(CourseFeedback feedback)
        {
            var feedbackExists = Validation.CourseFeedbackExists(_context,feedback.CourseId,feedback.TraineeId);
            if(feedbackExists)
            {
            if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    var IsValid = Validation.ValidateCourseFeedback(feedback,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _feedbackService.UpdateCourseFeedback(feedback,_context);
                        if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(UpdateCourseFeedback));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
       [HttpGet("trainee/{traineeId:int},{trainerId:int}")]
        public IActionResult GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId,int traineeId,int trainerId)
        {
            var feedbackExists = Validation.TraineeFeedbackExists(_context,courseId,traineeId,trainerId);
            if(feedbackExists)
            {
                try
                {
                    var result = _feedbackService.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId,traineeId,trainerId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
        [HttpPost("trainee")]
        public IActionResult CreateTraineeFeedback(TraineeFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateTraineeFeedback(feedback,_context);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create feedback. the feedback Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _feedbackService.CreateTraineeFeedback(feedback,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateTraineeFeedback));
                return Problem(ProblemResponse);
            }
        }

        [HttpPut("trainee")]
        public IActionResult UpdateTraineeFeedback(TraineeFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var feedbackExists = Validation.TraineeFeedbackExists(_context,feedback.CourseId,feedback.TraineeId,feedback.TrainerId);
            if(feedbackExists)
            {
                try
                {
                    var IsValid = Validation.ValidateTraineeFeedback(feedback,_context);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        var res = _feedbackService.UpdateTraineeFeedback(feedback,_context);
                        if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(UpdateCourseFeedback));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();

        }
    }
}