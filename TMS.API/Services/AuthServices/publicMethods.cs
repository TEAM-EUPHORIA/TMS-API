using TMS.API.Repositories;
using TMS.API.ViewModels;

namespace TMS.API.Services
{
    public partial class AuthService : IAuthService
    {
        private readonly UnitOfWork _repo;
        public AuthService(UnitOfWork repo)
        {
            _repo = repo;
        }
        public Dictionary<string, string> Login(LoginModel user)
        {
            var validation = _repo.Validation.ValidateLoginDetails(user);
            if (validation.ContainsKey("IsValid"))
            {
                var result = new Dictionary<string, string>();
                var dbUser = _repo.Users.GetUserByEmailAndPassword(user);
                string tokenString = GenerateTokenString(dbUser);
                result.Add("token", tokenString);
                return result;
            }
            return validation;
        }
    }
}