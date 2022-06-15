using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;
        private readonly Validation _validation;

        public UserController(UnitOfService service, ILogger<UserController> logger)
        {
            _logger = logger; 
            _userService = service.UserService;
            _validation = service.Validation;
        }

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
            var roleExists = _validation.RoleExists(roleId);
            if(roleExists)
            {
                try
                {
                    var result = _userService.GetUsersByRole(roleId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetAllUserByRole));
                    return Problem();
                }
            }
            return NotFound();
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
            var departmentExists = _validation.DepartmentExists(departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _userService.GetUsersByDepartment(departmentId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetAllUserByDepartment));
                    return Problem();
                }
            }
            return NotFound();
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
        public IActionResult GetUsersByDeptandrole(int departmentId,int roleId)
        {
            var departmentExists = _validation.DepartmentExists(departmentId);
            var roleExists = _validation.RoleExists(roleId);
            if(departmentExists && roleExists)
            {
                try
                {
                    var result = _userService.GetUsersByDeptandrole(departmentId,roleId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetUsersByDeptandrole));
                    return Problem();
                }
            }
            return NotFound();
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
        /// <response code="200">Returns a list of Users. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="userId"></param>
        [HttpGet("{userId:int}")]
        public IActionResult GetUserById(int userId)
        {
            var userExists = _validation.UserExists(userId);
            if(userExists)
            {
                try
                {
                    var result = _userService.GetUserById(userId);
                    if (result is not null) return Ok(result);
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetUserById));
                    return Problem();
                }
            }
            return NotFound();
        }

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
        // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _validation.ValidateUser(user);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create the user. The user Already exists.");
                if (IsValid.ContainsKey("IsValid"))
                {
                    user.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _userService.CreateUser(user);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The User was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(CreateUser));
                return Problem();
            }
        }

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
        // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult UpdateUser(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userExists = _validation.UserExists(user.Id);
            if(userExists)
            {
                try
                {
                    var IsValid = _validation.ValidateUser(user);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        user.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _userService.UpdateUser(user);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The User was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(UpdateUser));
                    return Problem();
                }
            }
            return NotFound();
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
        [HttpPut("disable/{userId:int}")]
        // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult DisableUser(int userId)
        {
            var userExists = _validation.UserExists(userId);
            if(userExists)
            {
                try
                {
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _userService.DisableUser(userId,currentUserId);
                    if (res) return Ok("The User was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(DisableUser));
                    return Problem();
                }
            }
            return NotFound();
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
        [Authorize (Roles = "Training Head, Training Coordinator, Trainer, Trainee")]
        public IActionResult DashboardData()
        {
            int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
            return Ok(_userService.Dashboard(currentUserId));
        }        
    }
}