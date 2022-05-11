
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;

namespace TMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController: ControllerBase
    {

        private readonly ILogger<DashboardController> _logger;
        private readonly AppDbContext _context;
        private readonly DashboardService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger, AppDbContext context, DashboardService dashboardService)
        {
            _logger = logger;
            _context = context;
            _dashboardService = dashboardService;
        }
        [HttpGet("GetUserCount")]
        public IActionResult getUserCount()
        {
           try
            {
                var result = _dashboardService.getUserCount();
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

        [HttpGet("GetCourseCount")]
        public IActionResult getCourseCount()
        {
           try
            {
                var result = _dashboardService.getCourseCount();
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

        [HttpGet("GetReviewCount")]
        public IActionResult getReviewCountByStatus()
        {
           try
            {
                var result = _dashboardService.getReviewCountByStatus();
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
        [HttpGet("GetDepartmentCount")]
        public IActionResult getDepartmentCount(){
            try
            {
                var result = _dashboardService.getDepartmentCount();
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
    }

}
