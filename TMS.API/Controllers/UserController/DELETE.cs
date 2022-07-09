using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;

namespace TMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public partial class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfService _service;

        public UserController(IUnitOfService service, ILogger<UserController> logger)
        {
            _logger = logger; 
            _service = service;
        }
        /// <summary>
        /// To disable a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/(userId:int)
        ///
        /// </remarks>
        /// <response code="200">If the user was disabled</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If User was not found. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="userId"></param>
        [HttpDelete("{userId:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult DisableUser(int userId)
        {
            // checks the user exists or not
            var userExists = _service.Validation.UserExists(userId);
            if(userExists)
            {
                try
                {
                    // getting the logged in user id
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    // calling service to disable the user
                    var res = _service.UserService.DisableUser(userId,currentUserId);
                    // response
                    if (res) return Ok(new {message = "The User was Disabled successfully"});
                }
                catch (InvalidOperationException ex)
                {
                    // An exception occurs only if IUnitOfService Dependency Injection (DI) is failed
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(UserController), nameof(DisableUser));
                    // response
                    return Problem("sorry somthing went wrong");
                }
            }
            // if the user is not found or already disabled
            // response
            return NotFound("NotFound");
        }
    }
}