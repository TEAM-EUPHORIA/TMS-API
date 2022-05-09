
using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


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
<<<<<<< HEAD
        // "GetCourse?CourseId={0}&UserId={1}"
=======
        /// <summary>
        /// This method is invoked when the trainer or coordinator wants to view a feedback
        /// </summary>
        /// <param name="cid">object</param>
        /// <param name="tid">object</param>
        /// <returns>returns bad request when object is null</returns>
>>>>>>> 4e842da6d0705ce1fbfbf488482944ed8545abce
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
<<<<<<< HEAD
                _logger.LogWarning("There was an error in getting all user by role. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
=======
                _logger.LogWarning("There was an error in getting Feedback. please check the Feedback service for more information");
                _logger.LogError($"error thrown by Feedback service " + ex.ToString());
>>>>>>> 4e842da6d0705ce1fbfbf488482944ed8545abce
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
<<<<<<< HEAD

        [HttpPost("Create")]
        public IActionResult CreateTraineeFeedback([FromForm] TraineeFeedbackDTO traineeFeedback)
        {
            if (traineeFeedback == null) return BadRequest("User is required");
=======
    /// <summary>
    /// This method is invoked when the trainer wants to create a feedback
    /// </summary>
    /// <param name="traineeFeedback">object</param>
    /// <returns>returns bad request when object is null</returns>
        [HttpPost("Create")]
        public IActionResult CreateTraineeFeedback([FromForm] TraineeFeedbackDTO traineeFeedback)
        {
            if (traineeFeedback == null) return BadRequest("Feedback is required");
>>>>>>> 4e842da6d0705ce1fbfbf488482944ed8545abce
            
            
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _traineefeedbackservice.CreateTFeedback(traineeFeedback);
<<<<<<< HEAD
                return Ok("The User was Created successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in creating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
=======
                return Ok("The Feedback was Created successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in creating the Feedback. please check the Feedback service for more information");
                _logger.LogError($"error thrown by Feedback service " + ex.ToString());
>>>>>>> 4e842da6d0705ce1fbfbf488482944ed8545abce
                return Problem("we are sorry, some thing went wrong");
            }

        }
<<<<<<< HEAD
        [HttpPut("Update")]
        public IActionResult UpdateTraineeFeedback([FromForm] TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null ) return BadRequest("User is required");
=======
        /// <summary>
        /// This method is invoked when the trainer wants to update a feedback
        /// </summary>
        /// <param name="traineeFeedback"></param>
        /// <returns>returns bad request when object is null</returns>
        [HttpPut("Update")]
        public IActionResult UpdateTraineeFeedback([FromForm] TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null ) return BadRequest("Feedback is required");
>>>>>>> 4e842da6d0705ce1fbfbf488482944ed8545abce
           
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _traineefeedbackservice.UpdateTFeedback(traineeFeedback);
<<<<<<< HEAD
                return Ok("The User was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
=======
                return Ok("The Feedback was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the Feedback. please check the Feedback service for more information");
                _logger.LogError($"error thrown by Feedback service " + ex.ToString());
>>>>>>> 4e842da6d0705ce1fbfbf488482944ed8545abce
                return Problem("we are sorry, some thing went wrong");
            }

        }
        // [HttpDelete("Disable")]
        // public IActionResult DisableUser([FromForm] User user)
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