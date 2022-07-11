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
        /// Update a assignment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/assignment
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         courseId* : int      
        ///         topicId* : int 
        ///         ownerId* : int 
        ///         base64* : string (the document in base64)
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the assignment was submitted. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>        
        /// <response code="404">If assignment was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="assignment"></param>
        [HttpPut("assignment")]
        
        [Authorize(Roles = "Trainer,Trainee")]
        public IActionResult UpdateAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var assignmentExists = _service.Validation.AssignmentExists(assignment.CourseId, assignment.TopicId, assignment.OwnerId);
            if (assignmentExists)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateAssignment(assignment);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.CourseService.UpdateAssignment(assignment,updatedBy);
                        if (res.ContainsKey("IsValid") && IsValid.ContainsKey("Exists")) return Ok(new { Response = "The Assignment was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(UpdateAssignment));
                }
                return Problem("sorry somthing went wrong");
            }
            return NotFound("Not found");
        }
    }
}