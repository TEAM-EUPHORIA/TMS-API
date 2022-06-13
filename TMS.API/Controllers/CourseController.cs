
using Microsoft.AspNetCore.Mvc;
using TMS.BAL;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseService _courseService;
        private readonly Validation _validation;

        public CourseController(UnitOfService service, ILogger<CourseController> logger)
        {
            _logger = logger;
            _courseService = service.CourseService;
            _validation = service.Validation;
        }
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
        public IActionResult GetCourses()
        {
            try
            {
                return Ok(_courseService.GetCourses());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCourses));
                return Problem();
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
        [Authorize(Roles = "Training Head, Training Coordinator, Trainer, Trainee")]
        public IActionResult GetCoursesByUserId(int userId)
        {
            var userExists = _validation.UserExists(userId);
            if(userExists)
            {
                try
                {
                    return Ok(_courseService.GetCoursesByUserId(userId));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCoursesByUserId));
                    return Problem();
                }
            }
            return NotFound();
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
        [Authorize(Roles = "Training Head, Training Coordinator, Trainer, Trainee")]
        public IActionResult GetCoursesByDepartmentId(int departmentId)
        {
            var departmentExists = _validation.DepartmentExists(departmentId);
            if(departmentExists)
            {
                try
                {
                    return Ok(_courseService.GetCoursesByDepartmentId(departmentId));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCoursesByDepartmentId));
                    return Problem();
                }
            }
            return NotFound();
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
            var courseExists = _validation.CourseExists(courseId);
            if(courseExists)
            {
                try
                {
                    var result = _courseService.GetCourseById(courseId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCourseById));
                    return Problem();
                }
            }
            return NotFound();
        }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateCourse(Course course)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _validation.ValidateCourse(course);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create course. The course already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    course.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _courseService.CreateCourse(course);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Course was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateCourse));
                return Problem();
            }
        }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateCourse(Course course)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var courseExists = _validation.CourseExists(course.Id);
            if(courseExists)
            {
                try
                {
                    var IsValid = _validation.ValidateCourse(course);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _courseService.UpdateCourse(course);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Course was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateCourse));
                    return Problem();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// To disable the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/disable/(courseId:int)
        ///
        /// </remarks>
        /// <response code="200">If the course was disabled </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        [HttpPut("disable/{courseId:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]        
        public IActionResult DisableCourse(int courseId)
        {
            var courseExists = _validation.CourseExists(courseId);
            if(courseExists)
            {
                try
                {
                    var res = _courseService.DisableCourse(courseId);
                    if (res) return Ok("The Course was successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(DisableCourse));
                    return Problem();
                }
            }
            return NotFound();

        }

        /// <summary>
        /// Gets a list of topics in a course by courseId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics
        ///
        /// </remarks>
        /// <response code="200">Returns a list of topics present in a course </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        [HttpGet("{courseId:int}/topics")]
        public IActionResult GetTopicsByCourseId(int courseId)
        {
            var courseExists = _validation.CourseExists(courseId);
            if(courseExists)
            {
                try
                {
                    return Ok(_courseService.GetTopicsByCourseId(courseId));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicsByCourseId));
                    return Problem();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Gets a single topic by courseId and topicId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a list of topics present in a course </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}")]
        public IActionResult GetTopicByIds(int courseId, int topicId)
        {
            var topicExists = _validation.TopicExists(topicId,courseId);
            if(topicExists)
            {
                try
                {
                    var result = _courseService.GetTopicById(courseId,topicId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicByIds));
                    return Problem();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Create a topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/topic
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         topicId* : int 
        ///         courseId* : int      
        ///         name* : string
        ///         duration* : string
        ///         content* : string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the topic was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="topic"></param>
        [HttpPost("topic")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateTopic(Topic topic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _validation.ValidateTopic(topic);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create topic. The topic already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    topic.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _courseService.CreateTopic(topic);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Topic was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateTopic));
                return Problem();
            }
        }

        /// <summary>
        /// Update a topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/topic
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///     {
        ///         topicId* : int 
        ///         courseId* : int      
        ///         name* : string
        ///         duration* : string
        ///         content* : string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the topic was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course or topic was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="topic"></param>
        [HttpPut("topic")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateTopic(Topic topic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var topicExists = _validation.TopicExists(topic.TopicId,topic.CourseId);
            if(topicExists)
            {
                try
                {
                    var IsValid = _validation.ValidateTopic(topic);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _courseService.UpdateTopic(topic);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Topic was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateTopic));
                    return Problem();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// To disable the topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId)/topics/disable/(topicId:int)
        ///
        /// </remarks>
        /// <response code="200">If the topic was disabled </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course or topic was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpPut("{courseId:int}/topics/disable/{topicId:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult DisableTopic(int courseId,int topicId)
        {
            var topicExists = _validation.TopicExists(topicId,courseId);
            if(topicExists)
            {
                try
                {
                    var res = _courseService.DisableTopic(courseId,topicId);
                    if (res) return Ok("The topic was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(DisableTopic));
                    return Problem();
                }
            }
            return NotFound();

        }

        /// <summary>
        /// To assign users to the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/assignUser
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
        /// <param name="data"></param>
        [HttpPut("assignUser")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult AssignUsersToCourse(AddUsersToCourse data)
        {
            var courseExists = _validation.CourseExists(data.CourseId);
            if(courseExists)
            {
                var result = _courseService.AddUsersToCourse(data);
                return Ok(result);
            }
            return NotFound();
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult RemoveUsersFromCourse(AddUsersToCourse data)
        {
            var courseExists = _validation.CourseExists(data.CourseId);
            if(courseExists)
            {
                var result = _courseService.RemoveUsersFromCourse(data);
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Gets a list of assigments in a topic by courseId and topicId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)/assignments
        ///
        /// </remarks>
        /// <response code="200">Returns a list of assigments present in a topic. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments")]
        public IActionResult GetAssignmentsByTopicId(int courseId, int topicId)
        {
            var topicExists = _validation.TopicExists(topicId,courseId);
            if(topicExists)
            {
                try
                {
                    return Ok(_courseService.GetAssignmentsByTopicId(topicId));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentsByTopicId));
                    return Problem();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Gets a single assigments in a topic by courseId, topicId and ownerId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:5001/Course/(courseId:int)/topics/(topicId:int)/assignments/(ownerId:int)
        ///
        /// </remarks>
        /// <response code="200">Returns a single assigments in a topic by courseId, topicId and ownerId. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If assignment was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments/{ownerId:int}")]
        public IActionResult GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            var assignmentExists = _validation.AssignmentExists(courseId,topicId,ownerId);
            if(assignmentExists)
            {
                try
                {
                    var result = _courseService.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId,topicId,ownerId);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentByCourseIdTopicIdAndOwnerId));
                    return Problem();
                }
            }
            return NotFound();
        }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainer,Trainee")]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _validation.ValidateAssignment(assignment);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create assignment. The assignment already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    assignment.CreatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                    var res = _courseService.CreateAssignment(assignment);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Assignment was submitted successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateAssignment));
            }
            return Problem();
        }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainer,Trainee")]
        public IActionResult UpdateAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var assignmentExists = _validation.AssignmentExists(assignment.CourseId,assignment.TopicId,assignment.OwnerId);
            if(assignmentExists)
            {
                try
                {
                    var IsValid = _validation.ValidateAssignment(assignment);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _courseService.UpdateAssignment(assignment);
                        if (res.ContainsKey("IsValid") && IsValid.ContainsKey("Exists")) return Ok(new { Response = "The Assignment was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateAssignment));
                }
                return Problem();
            }
            return NotFound();
        }
    }
}