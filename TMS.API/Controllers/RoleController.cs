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
        /// This method is invoked when the Coordinator/Head wants to view Roles
        /// </summary>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
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

        /// <summary>
        /// This method is invoked when the Coordinator/Head wants to view the User by Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetAllUserByRole
        ///     {
        ///        "userId": 2   
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("{roleId:int}")]
        public IActionResult GetAllUserByRole(int roleId)
        {
            var roleExists = Validation.RoleExists(_context,roleId);
            if(roleExists)
            {
                try
                {
                    var result = _roleService.GetUsersByRole(roleId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetAllUserByRole));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
    }
}