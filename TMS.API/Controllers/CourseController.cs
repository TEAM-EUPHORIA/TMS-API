
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
        /// This method is invoked when the Coordinator/Head wants to view all the courses
        /// </summary>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <returns></returns>
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
        /// This method is invoked when the Trainee/Trainer/Coordinator/Head wants to view a Courses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetCoursesByUserId
        ///     {
        ///        "UserId": 13        
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="userId"></param>
       /// <returns></returns>
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
        /// This method is invoked when the Coordinator/Head wants to view a courses by department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetCoursesByDepartmentId
        ///     {
        ///        "departmentId": 3
        ///        
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="departmentId"></param>
       /// <returns></returns>
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
        /// This method is invoked when the User wants to view a Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetCourseById
        ///     {
        ///        "courseId": 1,
        ///       
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
        /// This method is invoked when the Coordinator wants to create a Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /CreateCourse
        ///     {
        ///         "trainerId": 8,
        ///         "departmentId": 1,
        ///         "name": "JAVA",
        ///         "duration": "20 hrs",
        ///         "description": "Java Course Description"       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost("coures")]
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
        /// This method is invoked when the Coordinator wants to Update a Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UpdateCourse
        ///     { 
        ///         "id": 1,
        ///         "trainerId": 8,
        ///         "departmentId": 1,
        ///         "name": "Topic name",
        ///         "duration": "20 hrs",
        ///         "description": "Topic Description"
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="course"></param>
        /// <returns></returns>
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
        /// This method is invoked when the Coordinator wants to disable a course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /DisableCourse
        ///     {
        ///        "courseId": 1
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
        /// This method is invoked when the user wants to view a topic based on the specified course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetTopicsByCourseId
        ///     {
        ///        "courseId": 1
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="courseId"></param>
       /// <returns></returns>
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
        /// This method is invoked when the Coordinator/Head wants to view a Topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetTopicById
        ///     {
        ///        "topicId": 1
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="topicId"></param>
       /// <returns></returns>
        [HttpGet("topics/{topicId:int}")]
        public IActionResult GetTopicById(int topicId)
        {
            var topicExists = Validation.TopicExists(_context,topicId);
            if(topicExists)
            {
                try
                {
                    var result = _courseService.GetTopicById(topicId,_context);
                    if (result is not null) return Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicById));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
        /// <summary>
        /// This method is invoked when the Coordinator wants to create a topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /CreateTopic
        ///     {
        ///         "courseId": 3,
        ///         "name": "Inheritance",
        ///         "duration": "1 hrs",
        ///         "content": "Inheritance content"
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topic"></param>
        /// <returns></returns>
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
        /// This method is invoked when the Coordinator wants to Update a Topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UpdateTopic
        ///     {
        ///      "topicId":1,
        ///      "courseId": 3,
        ///      "name": "topic",
        ///      "duration": "1 hr",
        ///      "content": "topic content"
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topic"></param>
        /// <returns></returns>
        [HttpPut("topic")]
        public IActionResult UpdateTopic(Topic topic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var topicExists = Validation.TopicExists(_context,topic.TopicId);
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
        /// This method is invoked when the Coordinator wants to Disable a Topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /DisableTopic
        ///     {
        ///        "topicId": 1
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpPut("topics/disable/{topicId:int}")]
        public IActionResult DisableTopic(int topicId)
        {
            var topicExists = Validation.TopicExists(_context,topicId);
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
        /// This method is invoked when the User wants to view Assigment based on the Topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetAssignmentsByTopicId
        ///     {
        ///        "topicId": 1
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpGet("topics/{topicId:int}/assignments")]
        public IActionResult GetAssignmentsByTopicId(int topicId)
        {
            var topicExists = Validation.TopicExists(_context,topicId);
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
        /// This method is invoked when the Trainer wants to view Assignment 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetAssignmentByTopicIdAndOwnerId
        ///     {
        ///        "topicId": 1,
        ///        "ownerId": 13
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="courseId"></param>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet("topics/assignments/{courseId:int},{topicId:int},{ownerId:int}")]
        public IActionResult GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId,int topicId,int ownerId)
        {
            var assignmentExists = Validation.AssignmentExists(_context,courseId,topicId,ownerId);
            if(assignmentExists)
            {
                try
                {
                    var result = _courseService.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId,topicId,ownerId,_context);
                    if (result is not null) return Ok(result);
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentByCourseIdTopicIdAndOwnerId));
                }
                return Problem(ProblemResponse);
            }
            return NotFound();
        }
         /// <summary>
        /// This method is invoked when the trainee/trainer wants to create Assignment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /CreateAssignment
        ///     {
        ///          "topicId": 1,
        ///          "statusId": 1,
        ///          "ownerId": 13,
        ///          "base64": "string",
        ///          "document": "string",
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="assignment"></param>
        /// <returns></returns>
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
        /// This method is invoked when the trainee/trainer wants to Update Assignment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UpdateAssignment
        ///     {
        ///          "id": 1,    
        ///          "topicId": 1,
        ///          "statusId": 1,
        ///          "ownerId": 13,
        ///          "base64": "string",
        ///          "document": "string"
        ///       
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">Returns Not Found</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="assignment"></param>
        /// <returns></returns>
        /// ///
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