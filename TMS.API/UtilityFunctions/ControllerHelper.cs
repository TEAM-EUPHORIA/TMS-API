using System.Security.Claims;

namespace TMS.API.UtilityFunctions
{
    public static class ControllerHelper
    {
        public static int GetCurrentUserId(HttpContext context)
        {
            int userId = 0;
            if (context.User.Identity is ClaimsIdentity identity)
            {
                try
                {
                    userId = int.Parse(identity.FindFirst("UserId")!.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return userId;
        } 
    }
}