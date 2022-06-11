
using Microsoft.AspNetCore.Mvc;
using TMS.BAL;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : MyBaseController
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseService _courseService;

        public CourseController(ILogger<CourseController> logger, CourseService courseService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _courseService = courseService;
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
                return Ok(_courseService.GetCourses(_context));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCourses));
                return Problem(ProblemResponse);
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
        /// <response code="404">If user was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="userId"></param>
        [HttpGet("users/{userId:int}")]
        public IActionResult GetCoursesByUserId(int userId)
        {
            var userExists = Validation.UserExists(_context,userId);
            if(userExists)
            {
                try
                {
                    return Ok(_courseService.GetCoursesByUserId(userId,_context));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCoursesByUserId));
                    return Problem(ProblemResponse);
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
        public IActionResult GetCoursesByDepartmentId(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    return Ok(_courseService.GetCoursesByDepartmentId(departmentId,_context));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCoursesByDepartmentId));
                    return Problem(ProblemResponse);
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
        public IActionResult GetCourseById(int courseId)
        {
            var courseExists = Validation.CourseExists(_context,courseId);
            if(courseExists)
            {
                try
                {
                    var result = _courseService.GetCourseById(courseId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCourseById));
                    return Problem(ProblemResponse);
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
        ///         createdOn : dateTime
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">If the course was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="course"></param>
        [HttpPost("course")]
        public IActionResult CreateCourse(Course course)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateCourse(course,_context);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create course. The course already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.CreateCourse(course,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Course was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateCourse));
                return Problem(ProblemResponse);
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
        ///         updatedOn : dateTime
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the course was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="course"></param>
        [HttpPut("course")]
        public IActionResult UpdateCourse(Course course)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var courseExists = Validation.CourseExists(_context,course.Id);
            if(courseExists)
            {
                try
                {
                    var IsValid = Validation.ValidateCourse(course,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _courseService.UpdateCourse(course,_context);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Course was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateCourse));
                    return Problem(ProblemResponse);
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
        /// <response code="200">If the course was disabled / deleted. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        [HttpPut("disable/{courseId:int}")]
        public IActionResult DisableCourse(int courseId)
        {
            var courseExists = Validation.CourseExists(_context,courseId);
            if(courseExists)
            {
                try
                {
                    var res = _courseService.DisableCourse(courseId,_context);
                    if (res) return Ok("The Course was successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(DisableCourse));
                    return Problem(ProblemResponse);
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
            var courseExists = Validation.CourseExists(_context,courseId);
            if(courseExists)
            {
                try
                {
                    return Ok(_courseService.GetTopicsByCourseId(courseId,_context));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicsByCourseId));
                    return Problem(ProblemResponse);
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
        public IActionResult GetTopicByIds(int courseId,int topicId)
        {
            var topicExists = Validation.TopicExists(_context,topicId,courseId);
            if(topicExists)
            {
                try
                {
                    var result = _courseService.GetTopicById(topicId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicByIds));
                    return Problem(ProblemResponse);
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
        ///         createdOn : dateTime
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the topic was created. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="topic"></param>
        [HttpPost("topic")]
        public IActionResult CreateTopic(Topic topic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateTopic(topic,_context);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create topic. The topic already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.CreateTopic(topic,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Topic was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateTopic));
                return Problem(ProblemResponse);
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
        ///         updatedOn : dateTime
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the topic was updated. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course or topic was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="topic"></param>
        [HttpPut("topic")]
        public IActionResult UpdateTopic(Topic topic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var topicExists = Validation.TopicExists(_context,topic.TopicId,topic.CourseId);
            if(topicExists)
            {
                try
                {
                    var IsValid = Validation.ValidateTopic(topic,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _courseService.UpdateTopic(topic,_context);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Topic was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateTopic));
                    return Problem(ProblemResponse);
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
        /// <response code="200">If the topic was disabled / deleted. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If course or topic was not found. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpPut("{courseId:int}/topics/disable/{topicId:int}")]
        public IActionResult DisableTopic(int courseId,int topicId)
        {
            var topicExists = Validation.TopicExists(_context,topicId,courseId);
            if(topicExists)
            {
                try
                {
                    var res = _courseService.DisableTopic(topicId,_context);
                    if (res) return Ok("The topic was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(DisableTopic));
                    return Problem(ProblemResponse);
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
        public IActionResult AssignUsersToCourse(AddUsersToCourse data)
        {
            var courseExists = Validation.CourseExists(_context,data.CourseId);
            if(courseExists)
            {
                var result = _courseService.AddUsersToCourse(data,_context);
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
        public IActionResult RemoveUsersFromCourse(AddUsersToCourse data)
        {
            var courseExists = Validation.CourseExists(_context,data.CourseId);
            if(courseExists)
            {
                var result = _courseService.RemoveUsersFromCourse(data,_context);
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
        public IActionResult GetAssignmentsByTopicId(int courseId,int topicId)
        {
            var topicExists = Validation.TopicExists(_context,topicId,courseId);
            if(topicExists)
            {
                try
                {
                    return Ok(_courseService.GetAssignmentsByTopicId(topicId,_context));
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentsByTopicId));
                    return Problem(ProblemResponse);
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
        /// <returns></returns>
        [HttpGet("{courseId:int}/topics/{topicId:int}/assignments/{ownerId:int}")]
        public IActionResult GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId,int topicId,int ownerId)
        {
            var assignmentExists = Validation.AssignmentExists(_context,courseId,topicId,ownerId);
            if(assignmentExists)
            {
                try
                {
                    var result = _courseService.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId,topicId,ownerId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentByCourseIdTopicIdAndOwnerId));
                    return Problem(ProblemResponse);
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
        ///         createdOn : dateTime
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the assignment was submitted. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="assignment"></param>
        [HttpPost("assignment")]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateAssignment(assignment,_context);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create assignment. The assignment already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.CreateAssignment(assignment,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Assignment was submitted successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateAssignment));
            }
            return Problem(ProblemResponse);
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
        ///         createdOn : dateTime
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the assignment was submitted. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="assignment"></param>
        [HttpPut("assignment")]
        public IActionResult UpdateAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var assignmentExists = Validation.AssignmentExists(_context,assignment.CourseId,assignment.TopicId,assignment.OwnerId);
            if(assignmentExists)
            {
                try
                {
                    var IsValid = Validation.ValidateAssignment(assignment,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _courseService.UpdateAssignment(assignment,_context);
                        if (res.ContainsKey("IsValid") && IsValid.ContainsKey("Exists")) return Ok(new { Response = "The Assignment was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateAssignment));
                }
                return Problem(ProblemResponse);
            }
            return NotFound();
        }
    }
}