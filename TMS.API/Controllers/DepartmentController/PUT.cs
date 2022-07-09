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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
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
                        department.UpdatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.DepartmentService.UpdateDepartment(department);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Department was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentController), nameof(UpdateDepartment));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("Not Found");
        }   
    }
}