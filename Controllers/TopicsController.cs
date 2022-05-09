using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ILogger<TopicsController> _logger;
        private readonly CourseService _courseService;

        public TopicsController(ILogger<TopicsController> logger, CourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

         [HttpGet("GetTopicById/{id:int}")]
        public IActionResult GetTopicDetailsById(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Depatment id");
            try
            {
                var result = _courseService.GetTopicDetailsById(id);
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
        [HttpGet("GetTopicsByCourseId/{id:int}")]
        public IActionResult GetAllTopicByCourseId(int id)
        {
           
            try
            {
                var result = _courseService.GetAllTopicsByCourseId(id);
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

      
    }
}