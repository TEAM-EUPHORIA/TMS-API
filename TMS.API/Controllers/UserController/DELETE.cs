using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfService _service;
        public UserController(IUnitOfService service, ILogger<UserController> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _service = service ?? throw new ArgumentException(nameof(service));
        }
        /// <summary>
        /// To disable a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/disable/(userId:int)
        ///
        /// </remarks>
        /// <response code="200">If the user was disabled</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If User was not found. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="userId"></param>
        [HttpDelete("disable/{userId:int}")]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult DisableUser(int userId)
        {
            try
            {
                // checks the user exists or not
                var userExists = _service.Validation.UserExists(userId);
                if (userExists)
                {
                    // getting the logged in user id
                    int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                    // calling service to disable the user
                    var res = _service.UserService.DisableUser(userId, updatedBy);
                    // response
                    if (res) return Ok(new { message = "The User was Disabled successfully" });
                }
                // if the user is not found or already disabled
                // response
                return NotFound("NotFound");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}