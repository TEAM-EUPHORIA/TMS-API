using Microsoft.AspNetCore.Mvc;
using TMS.BAL;
using TMS.API.UtilityFunctions;
using Microsoft.AspNetCore.Authorization;
using TMS.API.ViewModels;

namespace TMS.API.Controllers
{
    [Authorize]
    public partial class CourseController : ControllerBase
    {
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
        
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult UpdateTopic([FromBody]Topic topic)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var topicExists = _service.Validation.TopicExists(topic.TopicId, topic.CourseId);
            if (topicExists)
            {
                try
                {
                    var IsValid = _service.Validation.ValidateTopic(topic);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        int updatedBy = ControllerHelper.GetCurrentUserId(this.HttpContext);
                        var res = _service.CourseService.UpdateTopic(topic,updatedBy);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Topic was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(UpdateTopic));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("NotFound");
        }
        [HttpPut("MarkAsComplete")]
        
        [Authorize(Roles = "Trainer")]
        public IActionResult MarkAsComplete([FromBody]TopicStatus topic)
        {
            var userId = ControllerHelper.GetCurrentUserId(this.HttpContext);
            var access = _service.Validation.ValidateCourseAccess(topic.CourseId, userId);
            var topicExists = _service.Validation.TopicExists(topic.TopicId, topic.CourseId);
            if (topicExists && access)
            {
                try
                {
                    var result = _context.Topics.Where(t => t.TopicId == topic.TopicId && t.CourseId == topic.CourseId).FirstOrDefault();
                    if(result != null)
                    {
                        result.Status = true;
                        _context.Update(result);
                        _context.SaveChanges();     
                        return Ok(new {Response = "The topic has been Marked as completed"});               
                    }
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(CourseController), nameof(UpdateTopic));
                    return Problem("sorry somthing went wrong");
                }
            }
            return NotFound("NotFound");
        }
    }
}