
using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseService _CourseService;

        public CourseController(ILogger<CourseController> logger, CourseService courseService)
        {
            _logger = logger;
            _CourseService = courseService;
        }

        [HttpGet("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            try
            {
                var result = _CourseService.GetAllCourses();
                if (result != null) return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all user by depatment. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
        
    

        [HttpPost("CreateCourse")]
        public IActionResult CreateCourse([FromForm] CourseDTO course)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _CourseService.CreateCourse(course);
                    return Ok("The course was Created successfully");
                }
                catch (System.Exception ex)
                {
                    _logger.LogWarning("There was an error in creating the course. please check the course service for more information");
                    _logger.LogError($"error thrown by course service " + ex.ToString());
                }
            }
 
            return Problem("we are sorry, some thing went wrong");
        }
          [HttpGet("GetTopicById/{id:int}")]
        public IActionResult GetTopicDetailsById(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Depatment id");
            try
            {
                var result = _CourseService.GetTopicDetailsById(id);
                if (result != "not found") return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all user by depatment. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
        [HttpGet("GetAllTopicsByCourseId/{id:int}")]
        public IActionResult GetAllTopicByCourseId(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Depatment id");

            try
            {
                var result = _CourseService.GetAllTopicsByCourseId(id);
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
         [HttpGet("GetCourseById/{id:int}")]
        public IActionResult GetCourseById(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Depatment id");
            try
            {
                var result = _CourseService.GetCourseById(id);
                if (result != "not found") return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all user by depatment. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
        // [HttpGet("GetUsersByRole/{id:int}")]
        // public IActionResult GetAllUserByRole(int id)
        // {
        //     if (id == 0) return BadRequest("Please provide a valid role id");
        //     try
        //     {
        //         var result = _userService.GetUsersByRole(id);
        //         if (result != null) return Ok(result);
        //         return NotFound("we are sorry, the thing you requested was not found");
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogWarning("There was an error in getting all user by role. please check the user service for more information");
        //         _logger.LogError($"error thrown by user service " + ex.ToString());
        //         return Problem("we are sorry, some thing went wrong");
        //     }
        // }

        // [HttpGet("GetUsersByDepartment/{id:int}")]
        // public IActionResult GetAllUserByDepartment(int DepatmentId)
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

      
        // [HttpPut("Update")]
        // public IActionResult UpdateUser([FromForm] UserDTO user)
        // {
        //     if (user == null || user.image == null) return BadRequest("User is required");
        //     user.Password = HashPassword.Sha256(user.Password);
        //     if (user.image != null)
        //     {
        //         user.Image = ImageService.imageToByteArray(user.image);
        //     }
        //     if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
        //     try
        //     {
        //         _userService.UpdateUser(user);
        //         return Ok("The User was Updated successfully");
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogWarning("There was an error in Updating the user. please check the user service for more information");
        //         _logger.LogError($"error thrown by user service " + ex.ToString());
        //         return Problem("we are sorry, some thing went wrong");
        //     }

        // }
        // [HttpDelete("Disable")]
        // public IActionResult DisableUser([FromForm] UserDTO user)
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