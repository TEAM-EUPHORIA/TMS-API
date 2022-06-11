using Microsoft.AspNetCore.Mvc;
using TMS.API.Controllers;
using TMS.API.Services;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API
{
    public class AuthController : MyBaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;

        public AuthController(AuthService authService, ILogger<AuthController> logger,AppDbContext dbContext):base(dbContext)
        {
            _logger = logger;
            _authService = authService;
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
        public IActionResult Login(LoginModel user)
        {
            var validation = Validation.ValidateLoginDetails(user,_context);
            try
            {
                if(validation.ContainsKey("IsValid"))
                {
                    var result = _authService.Login(user,_context);
                    if(result is not null) return (Ok(result));
                }
                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AuthController), nameof(Login));
                return Problem(ProblemResponse);
            }
        }
    }
}