
using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Models;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ReviewService _reviewService;

        public ReviewController(ILogger<ReviewController> logger, ReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [HttpGet("GetReviewById/{id:int}")]
        public IActionResult GetReviewById(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Review id");
            try
            {
                var result =_reviewService.GetReviewById(id);
                if (result != "not found") return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all review by id. please check the user service for more information");
                _logger.LogError($"error thrown by review service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
        [HttpGet("GetReviewByStatus/{id:int}")]
        public IActionResult GetReviewByStatus(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid status id");
            try
            {
                var result = _reviewService.GetReviewByStatus(id);
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

         
        [HttpPost("Create")]
        public IActionResult CreateReview([FromForm] ReviewDTO review)
        {
            if (review == null ) return BadRequest("User is required");
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _reviewService.CreateReview(review);
                return Ok("The Review was Created successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in creating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
        [HttpPut("Update")]
        public IActionResult UpdateReview([FromForm] Review review)
        {
            if (review == null ) return BadRequest("User is required");
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _reviewService.UpdateReview(review);
                return Ok("The User was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
        [HttpDelete("Disable")]
        public IActionResult DisableReview([FromForm] Review review)
        {
            if (review == null) return BadRequest("User is required");
            if (!ModelState.IsValid) return BadRequest("Please provide vaild User");
            try
            {
                _reviewService.DisableReview(review);
                return Ok("The User was Disabled successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Disabling the user. please check the user service for more information");
                _logger.LogError($"error thrown by user service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }


        }

    }}