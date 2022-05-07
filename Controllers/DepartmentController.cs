using Microsoft.AspNetCore.Mvc;
using TMS.API.DTO;
using TMS.API.Services;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly AppDbContext _context;

         private readonly DepartmentService  _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger, AppDbContext context, DepartmentService departmentService)
        {
            _logger = logger;
            _context = context;
            _departmentService= departmentService;
        }

        [HttpGet("GetAllDepartments")]
        [ActionName("GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            try
            {
                var result = _context.Departments.ToList();
                if (result != null) return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all Departments. please check the db for more information");
                _logger.LogError($"error:  " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }

           [HttpPost("Create")]
        public IActionResult CreateDepartment([FromForm] DepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentService.CreateDepartment(department);
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

        // [HttpDelete("Disable/{id:int}")]
        // public IActionResult DisableUser(int id)
        // {
        //     if (id == 0) return BadRequest("User is required");
        //     if (!ModelState.IsValid) return BadRequest("Please provide vaild User");
        //     try
        //     {
        //         _userService.DisableUser(id);

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