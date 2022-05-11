// using Microsoft.AspNetCore.Mvc;
// using TMS.API.Services;
// using TMS.API.UtilityFunctions;
// using TMS.BAL;

// namespace TMS.API.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class AssignmentController : MyBaseController
//     {
//         private readonly ILogger<AssignmentController> _logger;
//         private readonly UserService _userService;
//         public AssignmentController(ILogger<AssignmentController> logger, UserService userService)
//         {
//             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//             _userService = userService ?? throw new ArgumentNullException(nameof(userService));
//         }

//         [HttpGet("Role/{id:int}")]
//         public IActionResult GetAllUserByRole(int id)
//         {
//             if (id == 0) BadId();
//             try
//             {
//                 var result = _userService.GetUsersByRole(id);
//                 if (result != null) return Ok(result);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AssignmentController), nameof(GetAllUserByRole));
//                 return Problem(ProblemResponse);
//             }
//             catch (Exception ex)
//             {
//                 TMSLogger.EfCoreExceptions(ex, _logger, nameof(UserService), nameof(GetAllUserByRole));
//                 return Problem(ProblemResponse);
//             }
//             return Problem(ProblemResponse);
//         }

//         [HttpGet("Department/{id:int}")]
//         public IActionResult GetAllUserByDepartment(int id)
//         {
//             if (id == 0) return BadId();
//             try
//             {
//                 var result = _userService.GetUsersByDepartment(id);
//                 if (result != null) return Ok(result);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AssignmentController), nameof(GetAllUserByDepartment));
//                 return Problem(ProblemResponse);
//             }
//             catch (Exception ex)
//             {
//                 TMSLogger.EfCoreExceptions(ex, _logger, nameof(UserService), nameof(GetAllUserByDepartment));
//                 return Problem(ProblemResponse);
//             }
//             return Problem(ProblemResponse);
//         }

//         [HttpGet("/{id:int}")]
//         public IActionResult GetUserById(int id)
//         {
//             if (id == 0) return BadId();
//             try
//             {
//                 var result = _userService.GetUserById(id);
//                 if (result != null) return Ok(result);
//                 return NotFound();
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AssignmentController), nameof(GetUserById));
//                 return Problem(ProblemResponse);

//             }
//             catch (Exception ex)
//             {
//                 TMSLogger.EfCoreExceptions(ex, _logger, nameof(UserService), nameof(GetUserById));
//                 return Problem(ProblemResponse);
//             }
//         }

//         [HttpPost]
//         public IActionResult CreateUser(User user)
//         {
//             if (user == null) return BadObject();
//             if (!ModelState.IsValid) return BadRequest(ModelState);
//             try
//             {
//                 var res = _userService.CreateUser(user);
//                 if (res) return Ok("The User was Created successfully");
//                 return Problem(ProblemResponse);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AssignmentController), nameof(CreateUser));
//                 return Problem(ProblemResponse);
//             }
//             catch (Exception ex)
//             {
//                 TMSLogger.EfCoreExceptions(ex, _logger, nameof(UserService), nameof(CreateUser));
//                 return Problem(ProblemResponse);
//             }
//         }

//         [HttpPut]
//         public IActionResult UpdateUser(User user)
//         {
//             if (user == null) return BadObject();
//             if (!ModelState.IsValid) return BadRequest(ModelState);
//             try
//             {
//                 var res = _userService.UpdateUser(user);
//                 if (res) return Ok("The User was Updated successfully");
//                 return BadId();
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AssignmentController), nameof(UpdateUser));
//                 return Problem(ProblemResponse);
//             }
//             catch (Exception ex)
//             {
//                 TMSLogger.EfCoreExceptions(ex, _logger, nameof(UserService), nameof(UpdateUser));
//                 return Problem(ProblemResponse);
//             }

//         }

//         [HttpPut("Disable/{id:int}")]
//         public IActionResult DisableUser(int id)
//         {
//             if (id == 0) return BadId();
//             try
//             {
//                 var res = _userService.DisableUser(id);
//                 if (res) return Ok("The User was Disabled successfully");
//                 return BadId();
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AssignmentController), nameof(GetAllUserByRole));
//                 return Problem(ProblemResponse);
//             }
//             catch (Exception ex)
//             {
//                 TMSLogger.EfCoreExceptions(ex, _logger, nameof(UserService), nameof(DisableUser));
//                 return Problem(ProblemResponse);
//             }

//         }
//     }
// }