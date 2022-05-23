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
        private readonly AppDbContext _context;

        public UserController(ILogger<UserController> logger, UserService userService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("Role/{id:int}")]
        public IActionResult GetAllUserByRole(int id)
        {
            if (id == 0) BadId();
            try
            {
                var result = _userService.GetUsersByRole(id);
                if (result != null) return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetAllUserByRole));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetAllUserByRole));
            }
            return Problem(ProblemResponse);
        }
        
        [HttpGet("Department/{id:int}")]
        public IActionResult GetAllUserByDepartment(int id)
        {
            if (id == 0) return BadId();
            try
            {
                var result = _userService.GetUsersByDepartment(id);
                if (result != null) return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetAllUserByDepartment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetAllUserByDepartment));
            }
            return Problem(ProblemResponse);
        }
        
        [HttpGet("GetUsersByDepartmentAndRole/{departmentId:int},{roleId:int}")]
        public IActionResult GetUsersByDeptandrole(int departmentId,int roleId)
        {
            if (departmentId == 0 || roleId==0) BadId();
            try
            {
                var result = _userService.GetUsersByDeptandrole(departmentId,roleId);
                if (result!=null) return Ok(result);
                return BadId();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetUsersByDeptandrole));
            }
            catch(Exception ex)
            {
                
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUsersByDeptandrole));
            }
            return Problem(ProblemResponse);
        }

        [HttpGet("/User/{id:int}")]
        public IActionResult GetUserById(int id)
        {
            if (id == 0) return BadId();
            try
            {
                var result = _userService.GetUserById(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(GetUserById));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUserById));
            }
            return Problem(ProblemResponse);
        }

        [HttpPost("/Create")]
        public IActionResult CreateUser(User user)
        {
            if (user == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateUser(user);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _userService.CreateUser(user);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The User was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(CreateUser));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(CreateUser));
            }
            return Problem(ProblemResponse);
        }

        [HttpPut("/User")]
        public IActionResult UpdateUser(User user)
        {
            if (user == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateUser(user);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _userService.UpdateUser(user);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The User was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(UpdateUser));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(UpdateUser));
            }
            return Problem(ProblemResponse);

        }

        [HttpPut("/User/Disable/{id:int}")]
        public IActionResult DisableUser(int id)
        {
            if (id == 0) return BadId();
            try
            {
                var res = _userService.DisableUser(id);
                if (res) return Ok("The User was Disabled successfully");
                return BadId();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(DisableUser));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(DisableUser));
            }
            return Problem(ProblemResponse);

        }

        [HttpGet("/User/Dashboard")]
        public IActionResult DashboardData()
        {
            return Ok(_userService.GetStats());
        }        
    }
}