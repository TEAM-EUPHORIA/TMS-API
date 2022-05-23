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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        
        /// <param name="user"></param>
        /// <returns></returns>
       

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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="user"></param>
       /// <returns></returns>
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="id"></param>
       /// <returns></returns>
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <returns></returns>
        [HttpGet("/User/Dashboard")]
        public IActionResult DashboardData()
        {
            return Ok(_userService.GetStats());
        }        
    }
}