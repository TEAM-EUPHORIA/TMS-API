using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [Authorize]
    public partial class DepartmentController : ControllerBase
    {
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
        
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateDepartment([FromBody]Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateDepartment(department);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create department. The department already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.DepartmentService.CreateDepartment(department,createdBy);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Department was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentController), nameof(CreateDepartment));
                return Problem("sorry somthing went wrong");
            }
        }       
    }
}