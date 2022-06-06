using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API.Services
{
    public partial class AuthService
    {
        public Dictionary<string,string> Login(LoginModel user,AppDbContext dbContext)
        {
            var validation = Validation.ValidateLoginDetails(user,dbContext);
            if(validation.ContainsKey("IsValid"))
            {
                var result = new Dictionary<string,string>();
                var dbUser = dbContext.Users.Where(u => u.Email == user.Email && u.Password == HashPassword.Sha256(user.Password)).Include(u => u.Role).FirstOrDefault();
                if(dbUser is not null && dbUser.Role is not null)
                {
                    List<Claim> claims = GenerateClaims(dbUser,dbUser.Role.Name);
                    string tokenString = GenerateTokenString(claims);
                    result.Add("token", tokenString);
                    return result;
                }
            }
            return validation;
        }
    }
}