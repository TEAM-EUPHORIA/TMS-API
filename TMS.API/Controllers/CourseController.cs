
using Microsoft.AspNetCore.Mvc;
using TMS.BAL;
using TMS.API.Services;
using TMS.API.UtilityFunctions;


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
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetCourses
        ///     {
        ///        
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <returns></returns>
        [HttpGet("/Courses")]
        public IActionResult GetCourses()
        {
            try
            {
                return Ok(_courseService.GetCourses());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCourses));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCourses));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="userId"></param>
       /// <returns></returns>
        [HttpGet("/Courses/Users/{userId:int}")]
        public IActionResult GetCoursesByUserId(int userId)
        {
            try
            {
                return Ok(_courseService.GetCoursesByUserId(userId));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCoursesByUserId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCoursesByUserId));
            }
            return Problem(ProblemResponse);
        }
        /// <summary>
        /// This method is invoked when the coordinator/Head wants to view a courses by department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetCoursesByDepartmentId
        ///     {
        ///        "departmentId": 1
        ///        
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="departmentId"></param>
       /// <returns></returns>
        [HttpGet("/Courses/Departments/{departmentId:int}")]
        public IActionResult GetCoursesByDepartmentId(int departmentId)
        {
            try
            {
                return Ok(_courseService.GetCoursesByDepartmentId(departmentId));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCoursesByDepartmentId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCoursesByDepartmentId));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("/Courses/{courseId:int}")]
        public IActionResult GetCourseById(int courseId)
        {
            if (courseId == 0) return BadId();
            try
            {
                var result = _courseService.GetCourseById(courseId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetCourseById));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetCourseById));
            }
            return Problem(ProblemResponse);
        }
        /// <summary>
        /// This method is invoked when the Coordinator wants to create a Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /CreateCourse
        ///     {
        ///        "statusId": 1,
        ///        "trainerId": 8,
        ///        "departmentId": 2,
        ///        "name": "JAVA",
        ///        "duration": "11 hrs",
        ///        "description": "Java Description"
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost("Course")]
        public IActionResult CreateCourse(Course course)
        {
            if (course == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateCourse(course,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.CreateCourse(course);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Course was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateCourse));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(CreateCourse));
            }
            return Problem(ProblemResponse);
        }
          /// <summary>
        /// This method is invoked when the Coordinator wants to Update a Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UpdateCourse
        ///     {
        ///       "id": 1,
        ///       "statusId": null,
        ///       "trainerId": 8,
        ///       "departmentId": 3,
        ///       "name": "HTML 5 CSS 3",
        ///       "duration": "20 hrs",
        ///       "description": "description....."
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut("Course")]
        public IActionResult UpdateCourse(Course course)
        {
           if (course == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateCourse(course,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.UpdateCourse(course);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Course was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateCourse));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(UpdateCourse));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPut("/Courses/Disable/{courseId:int}")]
        public IActionResult DisableCourse(int courseId)
        {
            if (courseId == 0) return BadId();
            try
            {
                var res = _courseService.DisableCourse(courseId);
                if (res) return Ok("The Course was Disabled successfully");
                return BadId();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(DisableCourse));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(DisableCourse));
            }
            return Problem(ProblemResponse);

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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="courseId"></param>
       /// <returns></returns>
        [HttpGet("/Courses/{courseId:int}/Topics")]
        public IActionResult GetTopicsByCourseId(int courseId)
        {
            try
            {
                return Ok(_courseService.GetTopicsByCourseId(courseId));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicsByCourseId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetTopicsByCourseId));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        
       /// <param name="topicId"></param>
       /// <returns></returns>
        [HttpGet("Topics/{topicId:int}")]
        public IActionResult GetTopicById(int topicId)
        {
            if (topicId == 0) return BadId();
            try
            {
                var result = _courseService.GetTopicById(topicId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetTopicById));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetTopicById));
            }
            return Problem(ProblemResponse);
        }
        /// <summary>
        /// This method is invoked when the Coordinator wants to create a topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /CreateTopic
        ///     {
        ///         "courseId": 2,
        ///         "name": "Encapsulation",
        ///         "duration": "5 hrs",
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topic"></param>
        /// <returns></returns>
        [HttpPost("Topic")]
        public IActionResult CreateTopic(Topic topic)
        {
            if (topic == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateTopic(topic,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.CreateTopic(topic);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Topic was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateTopic));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(CreateTopic));
            }
            return Problem(ProblemResponse);
        }
         /// <summary>
        /// This method is invoked when the Coordinator wants to Update a Topic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UpdateTopic
        ///     {
        ///       "id": 1,
        ///       "courseId": 1,
        ///       "name": "HTML basics",
        ///       "duration": "30 mins",
        ///       "content": "update content...",
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topic"></param>
        /// <returns></returns>
        [HttpPut("Topic")]
        public IActionResult UpdateTopic(Topic topic)
        {
           if (topic == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateTopic(topic,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.UpdateTopic(topic);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Topic was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateTopic));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(UpdateTopic));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpPut("Topics/Disable/{topicId:int}")]
        public IActionResult DisableTopic(int topicId)
        {
            if (topicId == 0) return BadId();
            try
            {
                var res = _courseService.DisableTopic(topicId);
                if (res) return Ok("The topic was Disabled successfully");
                return BadId();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(DisableTopic));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(DisableTopic));
            }
            return Problem(ProblemResponse);

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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpGet("Topics/{topicId:int}/Assignments")]
        public IActionResult GetAssignmentsByTopicId(int topicId)
        {
            try
            {
                return Ok(_courseService.GetAssignmentsByTopicId(topicId));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentsByTopicId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetAssignmentsByTopicId));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="topicId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet("Topics/Assignments/{topicId:int},{ownerId:int}")]
        public IActionResult GetAssignmentByTopicIdAndOwnerId(int topicId,int ownerId)
        {
            if (topicId == 0) return BadId();
            try
            {
                var result = _courseService.GetAssignmentByTopicIdAndOwnerId(topicId,ownerId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(GetAssignmentByTopicIdAndOwnerId));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetAssignmentByTopicIdAndOwnerId));
            }
            return Problem(ProblemResponse);
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPost("Assignment")]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            if (assignment == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateAssignment(assignment,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.CreateAssignment(assignment);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Assignment was submitted successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(CreateAssignment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(CreateAssignment));
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
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPut("Assignment")]
        public IActionResult UpdateAssignment(Assignment assignment)
        {
           if (assignment == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateAssignment(assignment,_context);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _courseService.UpdateAssignment(assignment);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Assignment was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(CourseController), nameof(UpdateAssignment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(UpdateAssignment));
            }
            return Problem(ProblemResponse);
        }
    }
}