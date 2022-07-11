using TMS.API.Repositories;
using TMS.API.ViewModels;

namespace TMS.API.Services
{
    public partial class AuthService : IAuthService
    {
        private readonly IUnitOfWork _repo;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> result = new();
        public AuthService(IUnitOfWork repo,IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        public Dictionary<string, string> Login(LoginModel user)
        {
            var validation = _repo.Validation.ValidateLoginDetails(user);
            if (validation.ContainsKey("IsValid"))
            {
                var dbUser = _repo.Users.GetUserByEmailAndPassword(user);
                string tokenString = GenerateTokenString(dbUser);
                result.Add("token", tokenString);
                result.Add("Role",dbUser.Role!.Name);
                int rol = dbUser.Role.Id;
                int currentId = dbUser.Id;
                result.Add("RoleId", rol.ToString());
                result.Add("UserId", currentId.ToString());
                return result;
            }
            return validation;
        }
    }
}