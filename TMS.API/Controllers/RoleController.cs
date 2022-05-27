using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : MyBaseController
    {
        private readonly ILogger<RoleController> _logger;
        private readonly RoleService _roleService;

        public RoleController(ILogger<RoleController> logger, RoleService roleService, AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _roleService = roleService;
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            try
            {
                return Ok(_roleService.GetRoles(_context));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(RoleService), nameof(GetRoles));
                return Problem(ProblemResponse);
            }
        }
    }
}