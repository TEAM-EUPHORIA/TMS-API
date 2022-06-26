using TMS.API.ViewModels;

namespace TMS.API.Services
{
    public interface IAuthService
    {
        Dictionary<string, string> Login(LoginModel user);
    }
}