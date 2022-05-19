using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;

namespace TMS.API
{
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost("/login")]
        public IActionResult Login(LoginModel user)
        {
            if (user == null) return BadRequest();

            var dbUser = _context.Users.Where(u => u.Email == user.Email && u.Password == HashPassword.Sha256(user.Password)).Include(u => u.Role).FirstOrDefault();
            if (dbUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Email",dbUser.Email),
                    new Claim("Name", dbUser.FullName),
                    new Claim("Role", dbUser.Role.Name),
                    new Claim("RoleId", dbUser.RoleId.ToString()),
                    new Claim("UserId", dbUser.Id.ToString())
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { token = tokenString });
            }
            return Unauthorized();
        }
    }
}