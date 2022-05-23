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
        /// <summary>
        /// This method is invoked when the Coordinator/Head wants to view a Departments
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetDepartments
        ///     {    
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <returns></returns>
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
          /// <summary>
        /// This method is invoked when the Coordinator/Head wants to view all Departments
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetDepartmentById
        ///     {
        ///        "departmentId": 1 
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="departmentId"></param>
       /// <returns></returns>
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
        /// <summary>
        /// This method is invoked when the Coordinator wants to create a Department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /CreateDepartment
        ///     {
        ///          "name": "SQL"
        ///          
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        
        /// <param name="department"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method is invoked when the Coordinator wants to Update Department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /UpdateDepartment
        ///     {
        ///         "id": 1,
        ///         "name": "B",
        ///         
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
       /// <param name="department"></param>
       /// <returns></returns>
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
        /// <summary>
        /// This method is invoked when the Coordinator wants to Disable Department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /DisableDepartment
        ///     {
        ///        "departmentId": 1
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="id"></param>
        /// <returns></returns>
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