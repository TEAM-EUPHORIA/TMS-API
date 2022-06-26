using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IUnitOfService _service;
        public DepartmentController(IUnitOfService service, ILogger<DepartmentController> logger)
        {
            _logger = logger;
            _service = service;
        }
        /// <summary>
        /// Get all Departments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Department/departments
        /// 
        ///</remarks>
        /// <response code="200">Returns a list of Departments. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet("department")]
        //// [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetDepartments()
        {
            try
            {
                return Ok(_service.DepartmentService.GetDepartments());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartments));
                return Problem();
            }
        }
           
        /// <summary>
        /// Gets a single Department by departmentId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Department/(departmentId:int)
        ///    
        /// </remarks>
        /// <response code="200">Returns a single department. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">If department was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="departmentId"></param>
        [HttpGet("{departmentId:int}")]
        //// [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetDepartmentById(int departmentId)
        {
            var departmentExists = _service.Validation.DepartmentExists(departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _service.DepartmentService.GetDepartmentById(departmentId);
                    if (result is not null) return Ok(result);
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartmentById));
                    return Problem();
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
        ///     url : https://localhost:5001/Department/department
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///          name* : string 
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the department was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="department"></param>
        [HttpPost("department")]
        //[ValidateAntiForgeryToken]
        //// [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateDepartment(Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateDepartment(department);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create department. The department already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    //department.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.DepartmentService.CreateDepartment(department);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Department was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(CreateDepartment));
                return Problem();
            }
        }

        /// <summary>
        /// Update a department
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Department/department
        /// 
        ///     * fields are required
        ///     
        ///     body
        ///     {
        ///         id* : int,
        ///         name* : string 
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the department was updated.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Department was not found.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="department"></param>
        [HttpPut("department")]
        // [ValidateAntiForgeryToken]
        // // [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateDepartment(Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var departmentExists = _service.Validation.DepartmentExists(department.Id);
            if(departmentExists)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateDepartment(department);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        //department.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.DepartmentService.UpdateDepartment(department);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Department was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(UpdateDepartment));
                    return Problem();
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
        ///     url : https://localhost:5001/Department/disable/(departmentId:int)
        /// 
        /// </remarks>
        /// <response code="200">If the Department was disabled</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Department was not found.</response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="departmentId"></param>
        [HttpPut("disable/{departmentId:int}")]
        // [ValidateAntiForgeryToken]
        //// [Authorize(Roles = "Training Coordinator")]
        public IActionResult DisableDepartment(int departmentId)
        {
            var departmentExists = _service.Validation.DepartmentExists(departmentId);
            if(departmentExists)
            {
                try
                {
                    //int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    int currentUserId = 1;
                    var res = _service.DepartmentService.DisableDepartment(departmentId, currentUserId);
                    if (res) return Ok("The Department was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(DisableDepartment));
                    return Problem();
                }
            }
            return NotFound();
        }
    }
}