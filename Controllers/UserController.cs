
using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUsersById/{id:int}")]
        public IActionResult GetAllUserById(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Depatment id");
            try
            {
                var result = _userService.GetUsersById(id);
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
        [HttpGet("GetUsersByRole/{id:int}")]
        public IActionResult GetAllUserByRole(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid role id");
            try
            {
                var result = _userService.GetUsersByRole(id);
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

        [HttpGet("GetUsersByDepartment/{id:int}")]
        public IActionResult GetAllUserByDepartment(int DepatmentId)
        {
            if (DepatmentId == 0) return BadRequest("Please provide a valid Depatment id");
            try
            {
                var result = _userService.GetUsersByDepartment(DepatmentId);
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

        [HttpPost("Create")]
        public IActionResult CreateUser([FromForm] UserDTO user)
        {
            user.Password = HashPassword.Sha256(user.Password);
            if (user.image != null)
            {
                user.Image = ImageService.imageToByteArray(user.image);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.CreateUser(user);
                    return Ok("The User was Created successfully");
                }
                catch (System.Exception ex)
                {
                    _logger.LogWarning("There was an error in creating the user. please check the user service for more information");
                    _logger.LogError($"error thrown by user service " + ex.ToString());
                }
            }
            return Problem("we are sorry, some thing went wrong");

        }
        [HttpPut("Update")]
        public IActionResult UpdateUser([FromForm] UserDTO user)
        {
            if (user == null || user.image == null) return BadRequest("User is required");
            user.Password = HashPassword.Sha256(user.Password);
            if (user.image != null)
            {
                user.Image = ImageService.imageToByteArray(user.image);
            }
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _userService.UpdateUser(user);
                return Ok("The User was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }

        [HttpDelete("Disable/{id:int}")]
        public IActionResult DisableUser(int id)
        {
            if (id == 0) return BadRequest("User is required");
            if (!ModelState.IsValid) return BadRequest("Please provide vaild User");
            try
            {
                _userService.DisableUser(id);
                return Ok("The User was Disabled successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Disabling the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
    }
}