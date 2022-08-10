using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API.Controllers
{
    public partial class UserController : ControllerBase
    {
        /// <summary>
        /// Update a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/user
        ///
        ///     * fields are required
        ///     body
        ///     {
        ///          id* : int
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
        /// <response code="200">If the user was updated.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="user"></param>
        [HttpPut("user")]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult UpdateUser([FromBody] UpdateUserModel user)
        {
            if (user is null)
            {
                return BadRequest("user is required");
            }
            // checks if the model is valid or not
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                // checks the user exists or not
                var userExists = _service.Validation.UserExists(user.Id);
                if (userExists)
                {
                    // validating the user model and business logics
                    var modelValidation = _service.Validation.ValidateUpdtateUser(user);
                    if (modelValidation.ContainsKey("IsValid") && modelValidation.ContainsKey("Exists"))
                    {
                        // getting the logged in user id
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                        // calling service to update the user
                        var res = _service.UserService.UpdateUser(user, updatedBy);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists"))
                        {
                            // returning a list of users
                            var response = _service.UserService.GetUsersByRole(user.RoleId);
                            return Ok(new { response });
                        }
                    }
                    // if model is not valid
                    return BadRequest(modelValidation);
                }
                // if the user is not found or disabled
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