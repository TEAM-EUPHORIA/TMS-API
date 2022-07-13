using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class AuthService
    {
        /// <summary>
        /// generate encoded token for authorisation and identity purpose.
        /// </summary>
        /// <param name="dbuser"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="SecurityTokenEncryptionFailedException"></exception>
        private string GenerateTokenString(User dbUser)
        {
            var claims = GenerateClaims(dbUser);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            // var encryptingCredentials = new EncryptingCredentials(secretKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);

            var tokenOptions = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
              _configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
                new ClaimsIdentity(claims),
                DateTime.Now,
                DateTime.Now.AddDays(1),
                DateTime.Now,
                signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
        /// <summary>
        /// generate user details list of claims for token.
        /// </summary>
        /// <param name="dbuser"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        private static List<Claim> GenerateClaims(User dbUser)
        {

            return new List<Claim>
            {
                new Claim("Email",dbUser.Email),
                new Claim("Name", dbUser.FullName),
                new Claim("Role", dbUser.Role!.Name),
                new Claim(ClaimTypes.Role, dbUser.Role.Name),
                new Claim("RoleId", dbUser.RoleId.ToString()),
                new Claim("UserId", dbUser.Id.ToString())
            };
        }
    }
}