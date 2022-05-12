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
        
         [HttpGet("GetDepartmentNameByUserId/{id:int}")]
        public IActionResult GetDepartmentNameByUserId(int id)
        {
            if (id == 0) return BadRequest("Please provide a valid Depatment id");
            try
            {
                var result = _departmentService.GetDepartmentByUserId(id);
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

           [HttpPost("Create")]
        public IActionResult CreateDepartment(DepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentService.CreateDepartment(department);
                    return Ok("The Department was Created successfully");
                }
                catch (System.Exception ex)
                {
                    _logger.LogWarning("There was an error in creating the department. please check the user service for more information");
                    _logger.LogError($"error thrown by department service " + ex.ToString());
                }
            }
 
            return Problem("we are sorry, some thing went wrong");

        }
        [HttpPut("Update")]
        public IActionResult UpdateDepartment([FromForm] DepartmentDTO department)
        {
           
           
            if (!ModelState.IsValid) return BadRequest("Please provide vaild data");
            try
            {
                _departmentService.UpdateDepartment(department);
                return Ok("The departemnt was Updated successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Updating the department. please check the departemnt service for more information");
                _logger.LogError($"error thrown by department service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }

        [HttpDelete("Disable/{id:int}")]
        public IActionResult DisableDepartment(int id)
        {
            if (id == 0) return BadRequest("Department is required");
            if (!ModelState.IsValid) return BadRequest("Please provide vaild Department");
            try
            {
                _departmentService.DisableDepartment(id);

                return Ok("The department was Disabled successfully");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in Disabling the department. please check the department service for more information");
                _logger.LogError($"error thrown by department service " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }

        }
    }
}