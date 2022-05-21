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

        [HttpGet("/Departments")]
        public IActionResult GetDepartments()
        {
            try
            {
                return Ok(_departmentService.GetDepartments());
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartments));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(GetDepartments));
            }
            return Problem(ProblemResponse);
        }

        [HttpGet("/Department/{departmentId:int}")]
        public IActionResult GetDepartmentById(int departmentId)
        {
            if (departmentId == 0) return BadId();
            try
            {
                var result = _departmentService.GetDepartmentById(departmentId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(GetDepartmentById));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(GetDepartmentById));
            }
            return Problem(ProblemResponse);
        }
        
        [HttpPost("/Department")]
        public IActionResult CreateDepartment(Department department)
        {
            if (department == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateDepartment(department);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _departmentService.CreateDepartment(department);
                    if (res.ContainsKey("IsValid")) return Ok(new { Response = "The Department was Created successfully" });
                    return BadRequest(res);
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(CreateDepartment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(CreateDepartment));
            }
            return Problem(ProblemResponse);
        }
        
        [HttpPut("/Department")]
        public IActionResult UpdateDepartment(Department department)
        {
           if (department == null) return BadObject();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var IsValid = Validation.ValidateDepartment(department);
                if (IsValid.ContainsKey("IsValid"))
                {
                    var res = _departmentService.UpdateDepartment(department);
                    if (!res.ContainsKey("Invalid Id") && res.ContainsKey("IsValid")) return Ok(new { Response = "The Department was Updated successfully" });
                    return NotFound();
                }
                return BadRequest(IsValid);
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(UpdateDepartment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(UpdateDepartment));
            }
            return Problem(ProblemResponse);
        }

        [HttpPut("/Department/Disable/{id:int}")]
        public IActionResult DisableDepartment(int id)
        {
            if (id == 0) return BadId();
            try
            {
                var res = _departmentService.DisableDepartment(id);
                if (res) return Ok("The Department was Disabled successfully");
                return BadId();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(DepartmentController), nameof(DisableDepartment));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(DisableDepartment));
            }
            return Problem(ProblemResponse);

        }
    }
}