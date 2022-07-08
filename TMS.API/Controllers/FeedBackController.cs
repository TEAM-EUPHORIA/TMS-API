using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedBackController : ControllerBase
    {
        private readonly ILogger<FeedBackController> _logger;
        private readonly IUnitOfService _service;

        public FeedBackController(IUnitOfService service, ILogger<FeedBackController> logger)
        {
            _logger = logger;
            _service = service;
        }
        
        /// <summary>
        /// Gets a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/course/(courseId:int),(traineeId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single Feedback.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If Feedback was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        [HttpGet("course/{courseId:int},{traineeId:int}")]
        public IActionResult GetCourseFeedbackByCourseIdAndTraineeId(int courseId,int traineeId)
        {
            var feedbackExists = _service.Validation.CourseFeedbackExists(courseId,traineeId);
            if(feedbackExists)
            {
                try
                {
                    var result = _service.FeedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(GetCourseFeedbackByCourseIdAndTraineeId));
                    return Problem();
                }
            }
            return NotFound("NotFound");
        }
        
        /// <summary>
        ///  Create a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/course/feedback
        /// 
        ///     * fields are required
        /// 
        ///     {
        ///        courseId* : int,
        ///        traineeId* : int,
        ///        feedback* : string,
        ///        rating* : float
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="feedback"></param>
        [HttpPost("course/feedback")]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Trainee")]
        public IActionResult CreateCourseFeedback(CourseFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateCourseFeedback(feedback);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't submit the feedback. The feedback Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    feedback.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.FeedbackService.CreateCourseFeedback(feedback);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateCourseFeedback));
                return Problem();
            }
        }
        
        /// <summary>
        /// Update a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/course/feedback
        /// 
        ///     * fields are required
        /// 
        ///     {
        ///        courseId* : int,
        ///        traineeId* : int,
        ///        feedback* : string,
        ///        rating* : float 
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        [HttpPut("course/feedback")]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Trainee")]
        public IActionResult UpdateCourseFeedback(CourseFeedback feedback)
        {
            var feedbackExists = _service.Validation.CourseFeedbackExists(feedback.CourseId,feedback.TraineeId);
            if(feedbackExists)
            {
            if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    var IsValid = _service.Validation.ValidateCourseFeedback(feedback);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        feedback.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.FeedbackService.UpdateCourseFeedback(feedback);
                        if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(UpdateCourseFeedback));
                    return Problem();
                }
            }
            return NotFound("NotFound");
        }
        
        /// <summary>
        /// Gets a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/trainee/(courseId:int),(traineeId:int),(trainerId:int)
        /// 
        ///
        /// </remarks>
        /// <response code="200">Returns a feedback</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <param name="courseId"></param>
        /// <param name="traineeId"></param>
        /// <param name="trainerId"></param>
        [HttpGet("trainee/{courseId:int},{traineeId:int},{trainerId:int}")]
        public IActionResult GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId,int traineeId,int trainerId)
        {
            var feedbackExists = _service.Validation.TraineeFeedbackExists(courseId,traineeId,trainerId);
            if(feedbackExists)
            {
                try
                {
                    var result = _service.FeedbackService.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId,traineeId,trainerId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId));
                    return Problem();
                }
            }
            return NotFound("NotFound");
        }
        
        /// <summary>
        /// To create a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/FeedBack/trainee/feedback
        /// 
        ///     * fields are required
        ///
        ///     POST /CreateTraineeFeedback
        ///     {
        ///       courseId* : int,
        ///       traineeId* : int,
        ///       trainerId* : int,
        ///       feedback* : string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was submited. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        [HttpPost("trainee/feedback")]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Trainer")]
        public IActionResult CreateTraineeFeedback(TraineeFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateTraineeFeedback(feedback);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create feedback. the feedback Already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    feedback.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.FeedbackService.CreateTraineeFeedback(feedback);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateTraineeFeedback));
                return Problem();
            }
        }

        /// <summary>
        /// To update a Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/FeedBack/trainee/feedback
        /// 
        ///      * fields are required
        /// 
        ///     PUT /UpdateTraineeFeedback
        ///     {
        ///        courseId* : int,
        ///        traineeId* : int,
        ///        trainerId* : int,
        ///        feedback* : string  
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the Feedback was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Feedback was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="feedback"></param>
        [HttpPut("trainee/feedback")]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Trainer")]
        public IActionResult UpdateTraineeFeedback(TraineeFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var feedbackExists = _service.Validation.TraineeFeedbackExists(feedback.CourseId,feedback.TraineeId,feedback.TrainerId);
            if(feedbackExists)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateTraineeFeedback(feedback);
                    if (IsValid.ContainsKey("IsValid"))
                    {
                        feedback.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.FeedbackService.UpdateTraineeFeedback(feedback);
                        if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Feedback was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(UpdateCourseFeedback));
                    return Problem();
                }
            }
            return NotFound("NotFound");

        }
    }
}