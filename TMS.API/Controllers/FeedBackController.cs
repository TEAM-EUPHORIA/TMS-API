
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
        /// This method is invoked when the trainer/coordinator wants to view a feedback about Course.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetCourseFeedbackByCourseIdAndOwnerId
        ///     {
        ///        "CourseId": 1,
        ///        "OwnerId": 13
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="courseId"></param>
        /// <param name="ownwerId"></param>
        /// <returns>Returns the Course feedback when the given input isvalid</returns>
       // [HttpGet("/Feedback/{courseId:int},{ownwerId:int}")]
        [HttpGet("GetCourseFeedback/{courseId:int},{ownwerId:int}")]
        public IActionResult GetCourseFeedbackByCourseIdAndOwnerId(int courseId, int ownwerId)
        {
            if (courseId == 0 || ownwerId == 0) return BadId();
            try
            {
                var result = _feedbackService.GetCourseFeedbackByCourseIdAndOwnerId(courseId, ownwerId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
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
        ///  This method is invoked when the trainee wants to create a Course Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateCourseFeedback
        ///     {
        ///        "CourseId": 1,
        ///        "OwnerId": 18,
        ///         "Feedback":"Type feedback about course",
        ///         "Rating":4.5
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost("Create")]
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
        ///This method is invoked when the trainee wants to update a Course Feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateCourseFeedback
        ///     {
        ///        "CourseId": 1,
        ///        "OwnerId": 13,
        ///        "Feedback":"Type feedback about course",
        ///         "Rating":4.5
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="feedback"></param>
       /// <returns></returns>
        //[HttpPut("/Feedback/Course/Update")]
         [HttpPut("Update")]
        public IActionResult UpdateCourseFeedback(CourseFeedback feedback)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var feedbackExists = Validation.CourseFeedbackExists(_context,feedback.CourseId,feedback.TraineeId);
            if(feedbackExists)
            {
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
        /// This method is invoked when the trainee wants to view a feedback given by Trainer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetTraineeFeedbackByTrainerIdAndTraineeId
        ///     {
        ///        "traineeId": 13,
        ///        "trainerId": 8
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="traineeId"></param>
       /// <param name="trainerId"></param>
       /// <returns></returns>
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
         /// <summary>
        /// This method is invoked when the trainer wants to create a feedback about Trainee
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateTraineeFeedback
        ///     {
        ///        "statusId": 0,
        ///        "traineeId": 0,
        ///        "trainerId": 0,
        ///        "courseId": 0,
        ///        "feedback": "Type feedback about Trainee",
        ///        "isDisabled": true,
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost("Trainee/Create")]
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
                else{
                     return BadRequest(IsValid);
                }
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(FeedBackController), nameof(CreateTraineeFeedback));
                return Problem(ProblemResponse);
            }
        }
       /// <summary>
        /// This method is invoked when the trainer wants to Update a feedback about Trainee
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateTraineeFeedback
        ///     {
        ///         "id": 0,
        ///         "statusId": 1,
        ///         "traineeId": 13,
        ///         "trainerId": 8,
        ///         "courseId": 1,
        ///         "feedback": "Type feedback about Trainee",
        ///         "isDisabled": true,
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPut("Trainee/Update")]
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