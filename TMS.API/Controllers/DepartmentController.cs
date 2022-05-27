using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : MyBaseController
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DepartmentService  _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger, DepartmentService departmentService,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _departmentService= departmentService;
        }

        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            try
            {
                return Ok(_departmentService.GetDepartments(_context));
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartments));
                return Problem(ProblemResponse);
            }
        }

        [HttpGet("{departmentId:int}")]
        public IActionResult GetDepartmentById(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    var result = _departmentService.GetDepartmentById(departmentId,_context);
                    if (result is not null) return Ok(result);
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartmentById));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
        
        [HttpPost("department")]
        public IActionResult CreateDepartment(Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateDepartment(department,_context);
                if (IsValid.ContainsKey("Exists")) return BadRequest("Can't create department. The department already exists");
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _departmentService.CreateDepartment(department,_context);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Department was Created successfully" });
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(CreateDepartment));
                return Problem(ProblemResponse);
            }
        }
        
        [HttpPut("department")]
        public IActionResult UpdateDepartment(Department department)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var departmentExists = Validation.DepartmentExists(_context,department.Id);
            if(departmentExists)
            {
                try
                {
                    var IsValid = Validation.ValidateDepartment(department,_context);
                    if (IsValid.ContainsKey("IsValid") && IsValid.ContainsKey("Exists"))
                    {
                        var res = _departmentService.UpdateDepartment(department,_context);
                        if (res.ContainsKey("IsValid") && res.ContainsKey("Exists")) return Ok(new { Response = "The Department was Updated successfully" });
                    }
                    return BadRequest(IsValid);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(UpdateDepartment));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }

        [HttpPut("disable/{departmentId:int}")]
        public IActionResult DisableDepartment(int departmentId)
        {
            var departmentExists = Validation.DepartmentExists(_context,departmentId);
            if(departmentExists)
            {
                try
                {
                    var res = _departmentService.DisableDepartment(departmentId,_context);
                    if (res) return Ok("The Department was Disabled successfully");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(DisableDepartment));
                    return Problem(ProblemResponse);
                }
            }
            return NotFound();
        }
    }
}