using Microsoft.AspNetCore.Mvc;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course
        ///
        /// </remarks>
        /// <response code="200">Returns a list of courses. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpGet]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetCourses()
        {
            try
            {
                return Ok(_service.CourseService.GetCourses());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets a list of courses assigned to user by userId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/users/(userId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a list of courses assigned to the user. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If there is problem in server </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="userId"></param>
        [HttpGet("users/{userId:int}")]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetCoursesByUserId(int userId)
        {
            try
            {
                var userExists = _service.Validation.UserExists(userId);
                if (userExists)
                {
                    return Ok(_service.CourseService.GetCoursesByUserId(userId));
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        [HttpGet("myCourses")]
        [Authorize(Roles = "Trainer, Trainee")]
        public IActionResult GetCoursesByUserId()
        {
            try
            {
                var userId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                var userExists = _service.Validation.UserExists(userId);
                if (userExists)
                {
                    return Ok(_service.CourseService.GetCoursesByUserId(userId));
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets a list of courses in a department by departmentId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/departments/(departmentId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a list of courses in a department. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If department was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="departmentId"></param>
        [HttpGet("departments/{departmentId:int}")]
        [Authorize(Roles = "Training Head, Training Coordinator")]
        public IActionResult GetCoursesByDepartmentId(int departmentId)
        {
            try
            {
                var departmentExists = _service.Validation.DepartmentExists(departmentId);
                if (departmentExists)
                {
                    return Ok(_service.CourseService.GetCoursesByDepartmentId(departmentId));
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// Gets a single course by courseId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single course. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        [HttpGet("{courseId:int}")]
        [Authorize(Roles = "Training Head, Training Coordinator, Trainer, Trainee")]
        public IActionResult GetCourseById(int courseId)
        {
            try
            {
                var courseExists = _service.Validation.CourseExists(courseId);
                VerifyAccess(courseId, out int userId, out bool access);
                if (!access) return Unauthorized("UnAuthorized, contect your admin");
                if (courseExists)
                {
                    var result = _service.CourseService.GetCourseById(courseId, userId);
                    if (result is not null) return Ok(result);
                }
                return NotFound("NotFound");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
        private void VerifyAccess(int courseId, out int userId, out bool access)
        {
            userId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
            var check = ControllerHelper.GetCurrentUserRole(this.HttpContext, _logger) == "Training Coordinator";
            access = check || _service.Validation.ValidateCourseAccess(courseId, userId);
        }
    }
}