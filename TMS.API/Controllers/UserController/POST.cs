using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    public partial class UserController : ControllerBase
    {
        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/user
        ///
        ///     * fields are required
        ///      body
        ///     {
        ///          roleId* : int
        ///          departmentId: int
        ///          fullName* : string
        ///          userName* : string
        ///          password* : string
        ///          email* : string
        ///          base64* : string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the user was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="user"></param>
        [HttpPost("user")]

        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                // checks if the model is valid or not
                if (!ModelState.IsValid) return BadRequest(ModelState);
                // validating the user model and business logics
                var modelValidation = _service.Validation.ValidateUser(user);
                // if the user already exists
                if (modelValidation.ContainsKey("Exists")) return BadRequest("Can't create the user. The user Already exists.");
                // if model is valid
                if (modelValidation.ContainsKey("IsValid"))
                {
                    // getting the logged in user id
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.UserService.CreateUser(user, createdBy);
                    if (res.ContainsKey("IsValid"))
                    {
                        var response = _service.UserService.GetUsersByRole(user.RoleId);
                        return Ok(new { response });
                    }
                }
                return BadRequest(modelValidation);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(UserController), nameof(CreateUser));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(CreateUser));
                return Problem("sorry somthing went wrong");
            }
        }

    }
}