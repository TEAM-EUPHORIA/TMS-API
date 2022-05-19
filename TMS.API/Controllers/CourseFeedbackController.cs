
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseFeedbackController : MyBaseController
    {
        private readonly ILogger<CourseFeedbackController> _logger;
        private readonly CourseFeedbackService _coursefeedbackservice;

        public CourseFeedbackController(ILogger<CourseFeedbackController> logger, CourseFeedbackService courseFeedbackService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _coursefeedbackservice = courseFeedbackService ?? throw new ArgumentNullException(nameof(courseFeedbackService));
        }
        /// <summary>
        /// This method is invoked when the trainer or coordinator wants to view a feedback
        /// </summary>
        /// <param name="cid">object</param>
        /// <param name="oid">object</param>
        /// <returns>returns bad request when object is null</returns>


        [HttpGet("GetCourseFeedbackBy/{cid:int},{oid:int}")]
        public IActionResult GetCourseFeedback(int cid, int oid)
        {
            if (cid == 0 || oid == 0) BadId();
            try
            {
                var result = _coursefeedbackservice.GetFeedbackByID(cid, oid);
                if (result != null) return Ok(result);
                return NotFound();

            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseFeedbackController), nameof(GetCourseFeedback));
                return Problem(ProblemResponse);
            }
            catch(Exception ex)
            {
                TMSLogger.EfCoreExceptions(ex,_logger,nameof(CourseFeedbackService),nameof(GetCourseFeedback));
                return Problem(ProblemResponse);
            }
            return Problem(ProblemResponse);
        }        
        /// <summary>
        /// This method is invoked when the trainee wants to create a feedback
        /// </summary>
        /// <param name="courseFeedback">object</param>
        /// <returns>returns bad request when object is null</returns>
        [HttpPost("Create")]
        public IActionResult CreateCourseFeedback(CourseFeedback courseFeedback)
        {
            if (courseFeedback == null) return BadObject();


            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
               var res = _coursefeedbackservice.CreateCFeedback(courseFeedback);
                if(res) return Ok("The CourseFeedback was created Successfully");
                return StatusCode(409,$"User Feedback already exist");
            }
             catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseFeedbackController), nameof(CreateCourseFeedback));
                return Problem(ProblemResponse);
            }
            catch(Exception ex)
            {
                TMSLogger.EfCoreExceptions(ex,_logger,nameof(CourseFeedbackService),nameof(CreateCourseFeedback));
                return Problem(ProblemResponse);
            }
            

        }
        // /// <summary>
        // /// This method is invoked when the trainee wants to update a feedback
        // /// </summary>
        // /// <param name="courseFeedback">object</param>
        // /// <returns>returns bad request when object is null</returns>
        // [HttpPut("Update")]
        // public IActionResult UpdateUser(CourseFeedback courseFeedback)
        // {
        //     if (courseFeedback == null) return BadRequest("Feedback is required");


        //     if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
        //     try
        //     {
        //         _coursefeedbackservice.UpdateCFeedback(courseFeedback);
        //         return Ok("The Feedback was Updated successfully");
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogWarning("There was an error in Updating the Feedback. please check the Feedback service for more information");
        //         _logger.LogError($"error thrown by Feedback service " + ex.ToString());
        //         return Problem("we are sorry, some thing went wrong");
        //     }

        // }
       
    }
}