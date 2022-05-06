using Microsoft.AspNetCore.Mvc;



namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly AppDbContext _context;

        public DepartmentController(ILogger<DepartmentController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
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
    }
}