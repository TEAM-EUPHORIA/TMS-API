using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class UserController : ControllerBase
    {
        /// <summary>
        /// Gets a list of users by role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/role/(roleId:int)
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of Users. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <param name="roleId"></param>
        [HttpGet("role/{roleId:int}")]
        public IActionResult GetAllUserByRole(int roleId)
        {
            try
            {
                var roleExists = _service.Validation.RoleExists(roleId);
                if (roleExists)
                {
                    var result = _service.UserService.GetUsersByRole(roleId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("NotFound");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets the list of Users by Department 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/department/(departmentId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a list of Users. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If user was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="departmentId"></param>
        [HttpGet("department/{departmentId:int}")]
        public IActionResult GetAllUserByDepartment(int departmentId)
        {
            try
            {
                var departmentExists = _service.Validation.DepartmentExists(departmentId);
                if (departmentExists)
                {
                    var result = _service.UserService.GetUsersByDepartment(departmentId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets the list of Users based on Department and Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/GetUsersByDepartmentAndRole/(departmentId:int,roleId:int)
        ///
        ///
        /// </remarks>
        /// <response code="200">Returns a list of Users. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If User was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        [HttpGet("GetUsersByDepartmentAndRole/{departmentId:int},{roleId:int}")]
        public IActionResult GetUsersByDeptandrole(int departmentId, int roleId)
        {
            var departmentExists = _service.Validation.DepartmentExists(departmentId);
            var roleExists = _service.Validation.RoleExists(roleId);
            if (departmentExists && roleExists)
            {
                var result = _service.UserService.GetUsersByDeptandRole(departmentId, roleId);
                if (result is not null) return Ok(result);
            }
            return NotFound("not found");
        }
        /// <summary>
        /// Get list of Users by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User(userId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single User. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="userId"></param>
        [HttpGet("{userId:int}")]
        public IActionResult GetUserById(int userId)
        {
            var userExists = _service.Validation.UserExists(userId);
            if (userExists)
            {
                var result = _service.UserService.GetUser(userId);
                if (result is not null) return Ok(result);
            }
            return NotFound("Not Found");
        }
        /// <summary>
        /// Gets Logged in user data
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User
        ///
        /// </remarks>
        /// <response code="200">Returns a single user. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        [HttpGet]
        public IActionResult GetUserProfile()
        {
            var userId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
            var userExists = _service.Validation.UserExists(userId);
            if (userExists)
            {
                var result = _service.UserService.GetUser(userId);
                if (result is not null) return Ok(result);
            }
            return NotFound("Not Found");
        }
        /// <summary>
        /// Gets a Dashboard
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/User/Dashboard
        ///
        /// </remarks>
        /// <response code="200">Returns the Dashboard</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="500">If there is problem in server.</response>
        [HttpGet("Dashboard")]
        public IActionResult DashboardData()
        {
            int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
            return Ok(_service.UserService.Dashboard(currentUserId));
        }
    }
}