
using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseFeedbackController : ControllerBase
    {
        private readonly ILogger<CourseFeedbackController> _logger;
        private readonly CourseFeedbackService _coursefeedbackservice;

        public CourseFeedbackController(ILogger<CourseFeedbackController> logger, CourseFeedbackService courseFeedbackService)
        {
            _logger = logger;
            _coursefeedbackservice = courseFeedbackService;
        }
        // "GetCourse?CourseId={0}&UserId={1}"
        [HttpGet ("GetCourseFeedbackBy/{cid:int},{oid:int}") ]
        public IActionResult GetCourseFeedback(int cid,int oid)
        {
            if (cid ==0 || oid==0) return BadRequest("Please provide a valid courseid and userid");
            try
            {
                var result = _coursefeedbackservice.GetFeedbackByID(cid,oid);
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
        public IActionResult CreateCourseFeedback([FromForm] CourseFeedbackDTO courseFeedback)
        {
            if (courseFeedback == null) return BadRequest("User is required");
            
            
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _coursefeedbackservice.CreateCFeedback(courseFeedback);
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
        public IActionResult UpdateUser([FromForm] CourseFeedback courseFeedback)
        {
            if (courseFeedback == null ) return BadRequest("User is required");
           
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _coursefeedbackservice.UpdateCFeedback(courseFeedback);
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