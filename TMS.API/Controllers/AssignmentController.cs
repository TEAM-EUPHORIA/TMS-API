using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : MyBaseController
    {
        private readonly ILogger<AssignmentController> _logger;
        private readonly AssignmentService _assignmentService;
        public AssignmentController(ILogger<AssignmentController> logger, AssignmentService assignmentService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _assignmentService = assignmentService ?? throw new ArgumentNullException(nameof(assignmentService));
        }

        [HttpPost]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            if (assignment == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateAssignment(assignment);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _assignmentService.CreateAssignment(assignment);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Assignment was Submitted successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(UserController), nameof(CreateAssignment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(CreateAssignment));
            }
            return Problem(ProblemResponse);
        }
    }
}