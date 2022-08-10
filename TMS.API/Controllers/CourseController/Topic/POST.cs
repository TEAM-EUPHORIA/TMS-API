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
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult CreateTopic([FromBody] Topic topic)
        {
            if (topic is null)
            {
                return BadRequest("topic is required");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = _service.Validation.ValidateTopic(topic);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create topic. The topic already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    int createdBy = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                    var res = _service.CourseService.CreateTopic(topic, createdBy);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Topic was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}