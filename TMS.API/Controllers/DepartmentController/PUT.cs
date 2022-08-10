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
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
            if (department is null)
            {
                return BadRequest("department is required");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var departmentExists = _service.Validation.DepartmentExists(department.Id);
                if (departmentExists)
                {
                    var IsValid = _service.Validation.ValidateDepartment(department);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                        var res = _service.DepartmentService.UpdateDepartment(department, updatedBy);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Department was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}