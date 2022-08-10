using Microsoft.AspNetCore.Mvc;
using TMS.BAL;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// Lets the user to mark attendance
        /// </summary>
        /// <response code="200">If the assignment was submitted. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>        
        /// <response code="404">If assignment was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="attendance"></param>
        /// <returns></returns>
        [HttpPut("attendance")]
        [Authorize(Roles = "Trainer, Trainee")]
        public IActionResult MarkAttendance([FromBody] Attendance attendance)
        {
            if (attendance is null)
            {
                return BadRequest("attendance is required");
            }
            try
            {
                bool access = _service.Validation.ValidateCourseAccess(attendance.CourseId, attendance.OwnerId);
                if (!access) return Unauthorized("Unauthorized");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var IsValid = _service.Validation.ValidateAttendance(attendance!);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't mark. The attendance already exists");
                if (IsValid.ContainsKey("IsValid") && access)
                {
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                    attendance.CreatedBy = currentUserId;
                    attendance.OwnerId = currentUserId;
                    var res = _service.CourseService.MarkAttendance(attendance);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The attendance was marked successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}