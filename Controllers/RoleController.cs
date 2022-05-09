using Microsoft.AspNetCore.Mvc;



namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly AppDbContext _context;

        public RoleController(ILogger<RoleController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetAllRoles")]
        [ActionName("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var result = _context.Roles.ToList();
                if (result != null) return Ok(result);
                return NotFound("we are sorry, the thing you requested was not found");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("There was an error in getting all roles. please check the db for more information");
                _logger.LogError($"error:  " + ex.ToString());
                return Problem("we are sorry, some thing went wrong");
            }
        }
    }
}