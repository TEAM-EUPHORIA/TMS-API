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
        /// Create a assignment
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
        /// <response code="500">If there is problem in server. </response>
        /// <param name="assignment"></param>
        [HttpPost("assignment")]
        
        [Authorize(Roles = "Trainer,Trainee")]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateAssignment(assignment);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create assignment. The assignment already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.CourseService.CreateAssignment(assignment,createdBy);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Assignment was submitted successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(CreateAssignment));
            }
            return Problem("sorry somthing went wrong");
        }
    }
}