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
        /// Create a course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/course
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         departmentId* : int (department for the course)
        ///         trainerId* : int      
        ///         name* : string
        ///         duration* : string
        ///         description* : string
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If the course was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="course"></param>
        [HttpPost("course")]
        
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateCourse(Course course)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateCourse(course);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create course. The course already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _service.CourseService.CreateCourse(course,createdBy);
                    if (res.ContainsKey("IsValid"))
                    {
                        return Ok(new { Response = "The Course was Created successfully" });
                    }
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(CreateCourse));
                return Problem("sorry somthing went wrong");
            }
        }
    }
}