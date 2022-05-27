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
        
        [HttpGet("getUsersByDepartmentAndRole/{departmentId:int},{roleId:int}")]
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

        [HttpGet("User/Dashboard")]
        public IActionResult DashboardData()
        {
            return Ok(_userService.HeadDashboard(_context));
        }        
    }
}