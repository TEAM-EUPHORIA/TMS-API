using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthService> _logger;
        public AuthService(AppDbContext context, ILogger<AuthService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
       
        public Dictionary<string,string> Login(LoginModel user)
        {
            var validation = Validation.ValidateLoginDetails(user,_context);
            var result = new Dictionary<string,string>();
            try
            {
                if(validation.ContainsKey("IsValid")){
                    var dbUser = _context.Users.Where(u => u.Email == user.Email && u.Password == HashPassword.Sha256(user.Password)).Include(u => u.Role).FirstOrDefault();
                    if (dbUser != null)
                    {
                        List<Claim> claims = GenerateClaims(dbUser);
                        string tokenString = GetToken(claims);
                        result.Add("token", tokenString);
                        result.Add("IsValid","true");
                    }
                    return result;
                }
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AuthService), nameof(Login));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(AuthService), nameof(Login));
                throw;
            }
            return validation;
        }

        private static string GetToken(List<Claim> claims)
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

        private static List<Claim> GenerateClaims(User dbUser)
        {
            return new List<Claim>
                {
                    new Claim("Email",dbUser.Email),
                    new Claim("Name", dbUser.FullName),
                    new Claim("Role", dbUser.Role.Name),
                    new Claim("RoleId", dbUser.RoleId.ToString()),
                    new Claim("UserId", dbUser.Id.ToString())
                };
        }
    }
}