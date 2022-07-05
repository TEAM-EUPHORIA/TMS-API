using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TMS.API.UtilityFunctions
{
    public static class ControllerHelper
    {
        public static int GetCurrentUserId(HttpContext context)
        {
            int userId = 0;
            if (context.User.Identity is ClaimsIdentity identity)
            {
                userId = int.Parse(identity.FindFirst("UserId")!.Value);
            }
            return userId;
        } 
    }
}