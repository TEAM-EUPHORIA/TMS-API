
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