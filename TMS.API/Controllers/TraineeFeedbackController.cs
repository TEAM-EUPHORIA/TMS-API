
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraineeFeedbackController : ControllerBase
    {
        private readonly ILogger<TraineeFeedbackController> _logger;
        private readonly TraineeFeedbackService _traineefeedbackservice;

        public TraineeFeedbackController(ILogger<TraineeFeedbackController> logger, TraineeFeedbackService traineeFeedbackService)
        {
            _logger = logger;
            _traineefeedbackservice = traineeFeedbackService;
        }
        /// <summary>
        /// This method is invoked when the trainer or coordinator wants to view a feedback
        /// </summary>
        /// <param name="cid">object</param>
        /// <param name="tid">object</param>
        /// <returns>returns bad request when object is null</returns>
        [HttpGet ("GetCourseFeedbackBy/{cid:int},{tid:int}") ]
        public IActionResult GetTraineeFeedback(int cid,int tid)
        {
            if (cid ==0 || tid==0 ) return BadRequest("Please provide a valid courseid and userid");
            try
            {
                var result = _traineefeedbackservice.GetTraineeFeedbackByID(cid,tid);
                if (result != null) return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting Feedback. please check the Feedback service for more information");
                _logger.LogError($"error thrown by Feedback service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }

        // [HttpGet("GetUsersByDepartment/{id:int}")]
        // public IActionResult GetAllUserByDepatment(int DepatmentId)
        // {
        //     if (DepatmentId == 0) return BadRequest("Please provide a valid Depatment id");
        //     try
        //     {
        //         var result = _userService.GetUsersByDepartment(DepatmentId);
        //         if (result != null) return Ok(result);
        //         return NotFound("we are sorry, the thing you requested was not found");
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogWarning("There was an error in getting all user by depatment. please check the user service for more information");
        //         _logger.LogError($"error thrown by user service " + ex.ToString());
        //         return Problem("we are sorry, some thing went wrong");
        //     }
        // }
    /// <summary>
    /// This method is invoked when the trainer wants to create a feedback
    /// </summary>
    /// <param name="traineeFeedback">object</param>
    /// <returns>returns bad request when object is null</returns>
        [HttpPost("Create")]
        public IActionResult CreateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null) return BadRequest("Feedback is required");
            
            
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _traineefeedbackservice.CreateTFeedback(traineeFeedback);
                return Ok("The Feedback was Created successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in creating the Feedback. please check the Feedback service for more information");
                _logger.LogError($"error thrown by Feedback service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
        /// <summary>
        /// This method is invoked when the trainer wants to update a feedback
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <returns>returns bad request when object is null</returns>
        [HttpPut("Update")]
        public IActionResult UpdateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null ) return BadRequest("Feedback is required");
           
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _traineefeedbackservice.UpdateTFeedback(traineeFeedback);
                return Ok("The Feedback was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the Feedback. please check the Feedback service for more information");
                _logger.LogError($"error thrown by Feedback service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
        // [HttpDelete("Disable")]
        // public IActionResult DisableUser(User user)
        // {
        //     if (user == null) return BadRequest("User is required");
        //     if (!ModelState.IsValid) return BadRequest("Please provide vaild User");
        //     try
        //     {
        //         _userService.DisableUser(user);
        //         return Ok("The User was Disabled successfully");
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogWarning("There was an error in Disabling the user. please check the user service for more information");
        //         _logger.LogError($"error thrown by user service " + ex.ToString());
        //         return Problem("we are sorry, some thing went wrong");
        //     }

        // }
    }
}