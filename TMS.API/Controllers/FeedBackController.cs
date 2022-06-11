
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

        public FeedBackController(ILogger<FeedBackController> logger, FeedbackService feedbackService, AppDbContext dbContext) : base(dbContext)
        {
            _logger = logger;
            _feedbackService = feedbackService;
        }
        /// <summary>
        ///Gets a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url:https://localhost:5001/FeedBack/course/(courseId:int),(traineeId:int)
        ///
        /// </remarks>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="200">Returns a single Feedback.</response>
        /// <response code="404">If Feedback was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <returns>Returns the Course feedback when the given input isvalid</returns>
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
       /// <summary>
        ///  Create a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url:https://localhost:5001/FeedBack/course/feedback
        /// 
        ///     * fields are required
        /// 
        ///     POST /CreateCourseFeedback
        ///     {
        ///        CourseId*: int,
        ///        TraineeId*: int,
        ///        Feedback*:string,
        ///         Rating*:float
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="200">If the Feedback was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost("course/feedback")]
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
       /// <summary>
       /// Update a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url:https://localhost:5001/FeedBack/course/feedback
        /// 
        ///     * fields are required
        /// 
        ///     PUT /UpdateCourseFeedback
        ///     {
        ///        CourseId*: int,
        ///        TraineeId*: int,
        ///        Feedback*:string,
        ///         Rating*:float
        ///       
        ///     }
        ///
        /// </remarks>
         /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
       /// <returns></returns>
         [HttpPut("course/feedback")]
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
        /// <summary>
        /// Gets a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url:https://localhost:5001/FeedBack/trainee/(courseId:int),(traineeId:int),(trainerId:int)
        /// 
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="courseId"></param>
       /// <param name="traineeId"></param>
       /// <param name="trainerId"></param>
       /// <returns></returns>
       [HttpGet("trainee/{courseId:int},{traineeId:int},{trainerId:int}")]
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
         /// <summary>
        ///To create a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url:https://localhost:5001/FeedBack/trainee/feedback
        /// 
        ///     * fields are required
        ///
        ///     POST /CreateTraineeFeedback
        ///     {
        ///       courseId*: int,
        ///       traineeId*: int,
        ///       trainerId*: int,
        ///        feedback*: string
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost("trainee/feedback")]
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
       /// <summary>
        /// To update a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url:https://localhost:5001/FeedBack/trainee/feedback
        /// 
        ///      * fields are required
        /// 
        ///     PUT /UpdateTraineeFeedback
        ///     {
        ///        courseId*: int,
        ///        traineeId*: int,
        ///        trainerId*: int,
        ///        feedback*: string
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPut("trainee/feedback")]
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