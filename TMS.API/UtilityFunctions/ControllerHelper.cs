using System.Security.Claims;

namespace TMS.API.UtilityFunctions
{
    public static class ControllerHelper
    {
        /// <summary>
        /// used to get logged in user id
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// int userId
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static int GetCurrentUserId(HttpContext context)
        {
            int userId = 0;
            if (context.User.Identity is ClaimsIdentity identity)
            {
                try
                {
                    userId = int.Parse(identity.FindFirst("UserId")!.Value);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return userId;
        } 
        public static string GetCurrentUserRole(HttpContext context)
        {
            string role = "";
            if (context.User.Identity is ClaimsIdentity identity)
            {
                try
                {
                    role = identity.FindFirst("Role")!.Value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return role;
        } 
    }
}