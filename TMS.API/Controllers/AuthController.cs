using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Controllers;
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
        private readonly Validation _validation;
        private readonly AuthService _authService;

        public AuthController(UnitOfService service, ILogger<AuthController> logger)
        {
            _logger = logger;
            _validation = service.Validation;
            _authService = service.AuthService;
        }
        
        /// <summary>
        /// Login to the system
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
        public IActionResult Login([FromBody]LoginModel user)
        {
            var validation = _validation.ValidateLoginDetails(user);
            try
            {
                if(validation.ContainsKey("IsValid"))
                {
                    var result = _authService.Login(user);
                    if(result is not null) return (Ok(result));
                }
                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AuthController), nameof(Login));
                return Problem();
            }
        }
    }
}