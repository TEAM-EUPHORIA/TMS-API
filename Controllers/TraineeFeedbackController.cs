
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
        // "GetCourse?CourseId={0}&UserId={1}"
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
                _logger.LogWarning("There was an error in getting all user by role. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
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

        [HttpPost("Create")]
        public IActionResult CreateTraineeFeedback([FromForm] TraineeFeedbackDTO traineeFeedback)
        {
            if (traineeFeedback == null) return BadRequest("User is required");
            
            
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _traineefeedbackservice.CreateTFeedback(traineeFeedback);
                return Ok("The User was Created successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in creating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
        [HttpPut("Update")]
        public IActionResult UpdateTraineeFeedback([FromForm] TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null ) return BadRequest("User is required");
           
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _traineefeedbackservice.UpdateTFeedback(traineeFeedback);
                return Ok("The User was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
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