using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : MyBaseController
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DepartmentService  _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger, DepartmentService departmentService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _departmentService= departmentService;
        }
        /// <summary>
        /// Get all Departments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///         url:https://localhost:5001/Department/departments
        ///</remarks>
        /// <response code="500">If there is problem in server. </response>
        /// <response code="200">Returns a list of Departments</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <returns></returns>
        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            try
            {
                return Ok(_departmentService.GetDepartments(_context));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartments));
                return Problem(ProblemResponse);
            }
        }

        /// <summary>
        /// Gets the list of Users by Department 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// url : https://localhost:5001/Department/(departmentId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a list of Users. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="404">If user was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <param name="departmentId"></param>
        [HttpGet("department/{departmentId:int}")]
        public IActionResult GetAllUserByDepartment(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _departmentService.GetUsersByDepartment(departmentId,_context);
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
        /// Gets a single Department by departmentId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url:https://localhost:5001/Department/(departmentId:int)
        ///    
        /// </remarks>
        /// <response code="200">Returns a single department. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="404">If department was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <param name="departmentId"></param>
        [HttpGet("{departmentId:int}")]
        public IActionResult GetDepartmentById(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _departmentService.GetDepartmentById(departmentId,_context);
                    if (result is not null) return Ok(result);
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartmentById));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
        /// <summary>
        /// Create a department
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url:https://localhost:5001/Department/department
        /// 
        ///     * fields are required
        /// 
        ///     POST /CreateDepartment
        ///     {
        ///          name*: string
        ///          
        ///     }
        ///
        /// </remarks>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="200">If the department was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost("department")]
        public IActionResult CreateDepartment(Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateDepartment(department,_context);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create department. The department already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _departmentService.CreateDepartment(department,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Department was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(CreateDepartment));
                return Problem(ProblemResponse);
            }
        }
        /// <summary>
        /// Update a department
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url:https://localhost:5001/Department/department
        /// 
        ///     * fields are required
        /// 
        ///     PUT /UpdateDepartment
        ///     {
        ///         id*: int,
        ///         name*: string
        ///         
        ///     }
        ///
        /// </remarks>
        /// <response code="500">If there is problem in server.</response>
        /// <response code="200">If the department was updated.</response>
        /// <response code="404">If Department was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
       /// <param name="department"></param>
       /// <returns></returns>
        [HttpPut("department")]
        public IActionResult UpdateDepartment(Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var departmentExists = Validation.DepartmentExists(_context,department.Id);
            if(departmentExists)
            {
                try
                {
                    var IsValid = Validation.ValidateDepartment(department,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _departmentService.UpdateDepartment(department,_context);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Department was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(UpdateDepartment));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
        /// <summary>
        /// To disable the department
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url:https://localhost:5001/Department/disable/(departmentId:int)
        /// 
        ///     * fields are required
        /// 
        ///     PUT /DisableDepartment
        ///     {
        ///        departmentId*: int
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">If there is problem in server. </response>
        /// <response code="200">If the Department was disabled / deleted.</response>
        /// <response code="404">If Department was not found.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPut("disable/{departmentId:int}")]
        public IActionResult DisableDepartment(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    var res = _departmentService.DisableDepartment(departmentId,_context);
                    if (res) return Ok("The Department was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(DisableDepartment));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
    }
}