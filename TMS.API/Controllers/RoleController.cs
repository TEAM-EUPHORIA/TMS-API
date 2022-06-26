using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IUnitOfService _service;

        public RoleController(IUnitOfService service, ILogger<RoleController> logger)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Gets a List of Roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Role/roles
        /// 
        /// </remarks>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="200">Returns the  Roles</response>
        /// <response code="404">If role was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        [HttpGet("roles")]
        // [Authorize(Roles = "Training Coordinator")]
        public IActionResult GetRoles()
        {
            try
            {
                return Ok(_service.RoleService.GetRoles());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(RoleService), nameof(GetRoles));
                return Problem();
            }
        }
    }
}