using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MOMController : ControllerBase
    {
        private readonly ILogger<MOMController> _logger;
        private readonly ReviewService _reviewService;

        public MOMController(ILogger<MOMController> logger, ReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

 [HttpGet("GetMOMById/{id:int}")]
        public IActionResult GetMOMById(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid MOM id");
            try
            {
                var result =_reviewService.GetMomById(id);
                if (result != "not found") return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting MOM by id. please check the user service for more information");
                _logger.LogError($"error thrown by review service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
        [HttpPost("Create")]
        public IActionResult CreateMOM(MOM mom)
        {
            if (mom == null ) return BadRequest("MOM is required");
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _reviewService.CreateMOM(mom);
                return Ok("The Review was Created successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in creating the MOM. please check the Review service for more information");
                _logger.LogError($"error thrown by review service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
         [HttpPut("Update")]
        public IActionResult UpdateMOM(MOM mom)
        {
            if (mom == null ) return BadRequest("MOM is required");
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _reviewService.UpdateMOM(mom);
                return Ok("The User was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the mom. please check the review service for more information");
                _logger.LogError($"error thrown by review service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }

                [HttpGet("GetMOMByStatus/{id:int}")]
        public IActionResult GetMOMByStatus(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid status id");
            try
            {
                var result = _reviewService.GetMOMByStatus(id);
                if (result != null) return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all review by status. please check the user service for more information");
                _logger.LogError($"error thrown by review service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }


    }
    }