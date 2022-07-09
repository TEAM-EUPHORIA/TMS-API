using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;

namespace TMS.API.Controllers
{
    [Authorize]
    public partial class DepartmentController : ControllerBase
    {
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
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetDepartments()
        {
            try
            {
                return Ok(_service.DepartmentService.GetDepartments());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentController), nameof(GetDepartments));
                return Problem("sorry somthing went wrong");
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
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetDepartmentById(int departmentId)
        {
            var departmentExists = _service.Validation.DepartmentExists(departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _service.DepartmentService.GetDepartmentById(departmentId);
                    if (result is not null) return Ok(result);
                    return NotFound("Not Found");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentController), nameof(GetDepartmentById));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }   
    }
}