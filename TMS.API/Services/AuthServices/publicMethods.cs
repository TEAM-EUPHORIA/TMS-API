using TMS.API.Repositories;
using TMS.API.ViewModels;

namespace TMS.API.Services
{
    public partial class AuthService : IAuthService
    {
        private readonly IUnitOfWork _repo;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> result = new();
        /// <summary>
        /// Constructor of AuthService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="configuration"></param>
        public AuthService(IUnitOfWork repo,IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
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
            var validation = _repo.Validation.ValidateLoginDetails(user);
            if (validation.ContainsKey("IsValid"))
            {
                var dbUser = _repo.Users.GetUserByEmailAndPassword(user);
                string tokenString = GenerateTokenString(dbUser);
                result.Add("token", tokenString);
                // result.Add("Role", dbUser.Role!.Name);
                return result;
            }
            return validation;
        }
    }
}