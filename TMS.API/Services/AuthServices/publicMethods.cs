using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API.Services
{
    public partial class AuthService : IAuthService
    {
        private readonly IUnitOfWork _repo;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly Dictionary<string, string> result = new();
        /// <summary>
        /// Constructor of AuthService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AuthService(IUnitOfWork repo, IConfiguration configuration,ILogger logger)
        {
            _repo = repo;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// used to Login as user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> Login(LoginModel user)
        {
            try
            {
                var validation = _repo.Validation.ValidateLoginDetails(user);
                if (validation.ContainsKey("IsValid"))
                {
                    var dbUser = _repo.Users.GetUserByEmailAndPassword(user);
                    string tokenString = GenerateTokenString(dbUser);
                    result.Add("token", tokenString);
                    return result;
                }
                return validation;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(AuthService),nameof(Login));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex,_logger,nameof(Login));
                throw;
            }
        }
    }
}