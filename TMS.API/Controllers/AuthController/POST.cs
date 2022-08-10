using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfService _service;
        /// <summary>
        /// Constructor for AuthController
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public AuthController(IUnitOfService service, ILogger<AuthController> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _service = service ?? throw new ArgumentException(nameof(service));
        }

        /// <summary>
        /// Used for loging into the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:5001/Auth/login
        ///     body   
        ///     {
        ///        "email": "user@domain.com",
        ///        "password": "abcd1234"  
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a jwt token. </response>
        /// <response code="401">Invalid credentials. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="user"></param>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                BadRequest("user is required");
            }
            try
            {
                var validation = _service.Validation.ValidateLoginDetails(user!);
                if (validation.ContainsKey("IsValid"))
                {
                    var result = _service.AuthService.Login(user!);
                    if (result is not null) return Ok(result);
                }
                return Unauthorized("Unauthorized user");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.RemovedTheConnectionStringInAppsettings(ex, _logger);
                return Problem("sorry somthing went wrong");
            }
        }
    }
}