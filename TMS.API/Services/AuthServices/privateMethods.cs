using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class AuthService
    {
        private static string GenerateTokenString(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
        private static List<Claim> GenerateClaims(User dbUser,string roleName)
        {
            return new List<Claim>
                {
                    new Claim("Email",dbUser.Email),
                    new Claim("Name", dbUser.FullName),
                    new Claim("Role", roleName),
                    new Claim("RoleId", dbUser.RoleId.ToString()),
                    new Claim("UserId", dbUser.Id.ToString())
                };
        }
    }
}