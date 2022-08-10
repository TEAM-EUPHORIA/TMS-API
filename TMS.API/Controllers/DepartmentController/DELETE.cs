using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public partial class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IUnitOfService _service;
        public DepartmentController(IUnitOfService service, ILogger<DepartmentController> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _service = service ?? throw new ArgumentException(nameof(service));
        }
        /// <summary>
        /// To disable the department
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Department/disable/(departmentId:int)
        /// 
        /// </remarks>
        /// <response code="200">If the Department was disabled</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="404">If Department was not found.</response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="departmentId"></param>
        [HttpDelete("disable/{departmentId:int}")]
        [Authorize(Roles = "Training Coordinator")]
        public IActionResult DisableDepartment(int departmentId)
        {
            try
            {
                var departmentExists = _service.Validation.DepartmentExists(departmentId);
                if (departmentExists)
                {
                    int currentUserId = ControllerHelper.GetCurrentUserId(this.HttpContext, _logger);
                    var res = _service.DepartmentService.DisableDepartment(departmentId, currentUserId);
                    if (res) return Ok(new { Response = "The Department was Deleted successfully" });
                }
                return NotFound("Not Found");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbRelatedProblemCheckTheConnectionString(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}