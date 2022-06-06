using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : MyBaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;
        public UserController(ILogger<UserController> logger, UserService userService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
        [HttpGet("role/{roleId:int}")]
        public IActionResult GetAllUserByRole(int roleId)
        {
            var roleExists = Validation.RoleExists(_context,roleId);
            if(roleExists)
            {
                try
                {
                    var result = _userService.GetUsersByRole(roleId,_context);
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
         /// <summary>
        /// This method is invoked when the Coordinator/Head wants to view User by Department 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetAllUserByDepartment
        ///     {
        ///        "Id": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("department/{departmentId:int}")]
        public IActionResult GetAllUserByDepartment(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _userService.GetUsersByDepartment(departmentId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetAllUserByDepartment));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
           /// <summary>
        /// This method is invoked when the Coordinator/Head wants to view a user based on Department and Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetUsersByDeptandrole
        ///     {
        ///        "departmentId": 1,
        ///        "roleId": 3
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("GetUsersByDepartmentAndRole/{departmentId:int},{roleId:int}")]
        public IActionResult GetUsersByDeptandrole(int departmentId,int roleId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            var roleExists = Validation.RoleExists(_context,roleId);
            if(departmentExists && roleExists)
            {
                try
                {
                    var result = _userService.GetUsersByDeptandrole(departmentId,roleId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetUsersByDeptandrole));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
          /// <summary>
        /// This method is invoked when the Coordinator/Head wants to view User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetUserById
        ///     {
        ///        "Id": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:int}")]
        public IActionResult GetUserById(int userId)
        {
            var userExists = Validation.UserExists(_context,userId);
            if(userExists)
            {
                try
                {
                    var result = _userService.GetUserById(userId,_context);
                    if (result is not null) return Ok(result);
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetUserById));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
         /// <summary>
        /// This method is invoked when the Coordinator wants to Create User / Head wants to create Coordinator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateUser
        ///     {
         ///          "roleId": 4,
        ///          "departmentId": 1,
        ///          "fullName": "Charles Benny",
        ///          "userName": "Charles",
        ///          "password": "password",
        ///          "email": "charlesb16@gmail.com",
        ///           "base64": "data:image/jpg;base64,.....",
        ///          "image": "image"
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="user"></param>
        /// <returns></returns>
       

        [HttpPost("user")]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateUser(user,_context);
                if(IsValid.ContainsKey("Exists")) return BadRequest("Can't create the user. The user Already exists.");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _userService.CreateUser(user,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The User was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(CreateUser));
                return Problem(ProblemResponse);
            }
        }
        /// <summary>
        /// This method is invoked when the Coordinator wants to Update User / Head wants to Update Coordinator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateUser
        ///     {
        ///          "id": 1,
        ///          "roleId": 1,
        ///          "departmentId": null,
        ///          "fullName": "warren mackenzie",
        ///          "userName": "warren",
        ///          "password": "password",
        ///          "email": "warren88@gmail.com",
        ///           "base64": "string",
        ///          "image": "string"
        ///          
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="user"></param>
       /// <returns></returns>
        [HttpPut("user")]
        public IActionResult UpdateUser(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userExists = Validation.UserExists(_context,user.Id);
            if(userExists)
            {
                try
                {
                    var IsValid = Validation.ValidateUser(user,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _userService.UpdateUser(user,_context);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The User was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(UpdateUser));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
       /// <summary>
        /// This method is invoked when the Coordinator wants to Disable User / Head wants to Diable Coordinator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /DisableUser
        ///     {
        ///        "Id": 1
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="userId"></param>
       /// <returns></returns>
        [HttpPut("disable/{userId:int}")]
        public IActionResult DisableUser(int userId)
        {
            var userExists = Validation.UserExists(_context,userId);
            if(userExists)
            {
                try
                {
                    var res = _userService.DisableUser(userId,_context);
                    if (res) return Ok("The User was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(DisableUser));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
         /// <summary>
        /// This method is invoked when the User wants to view Dashboard
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /DashboardData
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <returns></returns>
        [HttpGet("user/Dashboard")]
        public IActionResult DashboardData()
        {
            return Ok(_userService.HeadDashboard(_context));
        }        
    }
}