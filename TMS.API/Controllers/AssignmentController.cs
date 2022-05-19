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

        
    }
}