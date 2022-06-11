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
       /// <summary>
        /// Gets a List of Roles
        /// </summary>
        /// 
        /// url : https://localhost:5001/Role/roles
        /// 
        /// <response code="500">If there is problem in server.</response>
        /// <response code="200">Returns the  Roles</response>
        /// <response code="404">If role was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        
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