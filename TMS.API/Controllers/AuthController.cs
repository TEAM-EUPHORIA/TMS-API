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
        /// This method is invoked when the User wants to Login to the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login
        ///     {
        ///        "email": "Gabrielle.Pullman@tms.edu.in",
        ///        "password": "string"
        ///       
        ///     }
        ///
        /// </remarks>
        /// <response code="500">something has gone wrong on the website's server</response>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null/the server cannot or will not process the request due to something that is perceived to be a client error </response>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("/login")]
        public IActionResult Login(LoginModel user)
        {
            var validation = Validation.ValidateLoginDetails(user,_context);
            try
            {

                if(validation.ContainsKey("IsValid"))
                {
                    var result = _authService.Login(user);
                    if(result.ContainsKey("IsValid")) return (Ok(result));
                }

                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailed(ex, _logger, nameof(AuthController), nameof(Login));
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(AuthService), nameof(Login));
            }
            return Problem(ProblemResponse);
        }
    }
}