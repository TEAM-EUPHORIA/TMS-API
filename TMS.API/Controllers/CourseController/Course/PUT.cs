using Microsoft.AspNetCore.Mvc;
using TMS.BAL;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
        /// <summary>
        /// Update a course
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
        /// <response code="200">If the course was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="course"></param>
        [HttpPut("course")]

        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var courseExists = _service.Validation.CourseExists(course.Id);
                if (courseExists)
                {
                    var IsValid = _service.Validation.ValidateCourse(course);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.CourseService.UpdateCourse(course, updatedBy);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Course was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                return NotFound("NotFound");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(UpdateCourse));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(UpdateCourse));
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// To assign users to the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/assignUsers
        ///     
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         courseId* : int,
        ///         users* : (list of objects with userId and roleId)
        ///         [
        ///             {
        ///                 userId* : int,
        ///                 roleId* : int
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a list successfully assigned users. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.  </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        [HttpPut("assignUsers")]

        [Authorize(Roles = "Training Coordinator")]
        public IActionResult AssignUsersToCourse([FromBody] AddUsersToCourse data)
        {
            try
            {
                var courseExists = _service.Validation.CourseExists(data.CourseId);
                if (courseExists)
                {
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var result = _service.CourseService.AddUsersToCourse(data, currentUserId);
                    return Ok(result);
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(AssignUsersToCourse));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(AssignUsersToCourse));
                return Problem("sorry somthing went wrong");
            }
        }
        /// <summary>
        /// To remove users from the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/removeUsers
        ///     
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         courseId* : int,
        ///         users* : (list of objects with userId and roleId)
        ///         [
        ///             {
        ///                 userId* : int,
        ///                 roleId* : int
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a list successfully removed users. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="data"></param>
        [HttpPut("removeUsers")]

        [Authorize(Roles = "Training Coordinator")]
        public IActionResult RemoveUsersFromCourse([FromBody] AddUsersToCourse data)
        {
            try
            {
                var courseExists = _service.Validation.CourseExists(data.CourseId);
                if (courseExists)
                {
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var result = _service.CourseService.RemoveUsersFromCourse(data, currentUserId);
                    return Ok(result);
                }
                return NotFound("Not found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(RemoveUsersFromCourse));
                return Problem("sorry somthing went wrong");
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(RemoveUsersFromCourse));
                return Problem("sorry somthing went wrong");
            }
        }
    }
}