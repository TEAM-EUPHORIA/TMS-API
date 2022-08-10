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
        public AuthService(IUnitOfWork repo, IConfiguration configuration, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        /// <summary>
        /// used to Login as user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> Login(LoginModel user)
        {
            if (user is null)
            {
                throw new ArgumentException(nameof(user));
            }
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
    }
}