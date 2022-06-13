using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TMS.API.UtilityFunctions
{
    public static class ControllerHelper
    {
        public static int GetCurrentUserId(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            int userId = 0;
            if (identity != null)
            {
                int.TryParse(identity.FindFirst("Id").Value,out userId);
            }
            return userId;
        } 
    }
}