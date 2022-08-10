using System.Security.Claims;

namespace TMS.API.UtilityFunctions
{
    public static class ControllerHelper
    {
        /// <summary>
        /// used to get logged in user id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <returns>
        /// int userId
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static int GetCurrentUserId(HttpContext context, ILogger logger)
        {
            if (context is null)
            {
                throw new ArgumentException(nameof(context));
            }

            if (logger is null)
            {
                throw new ArgumentException(nameof(logger));
            }

            int userId = 0;
            if (context.User.Identity is ClaimsIdentity identity)
            {
                try
                {
                    userId = int.Parse(identity.FindFirst("UserId")!.Value);
                }
                catch (ArgumentException ex)
                {
                    TMSLogger.GeneralException(ex,logger,nameof(GetCurrentUserId));
                    logger.LogInformation(ex.ToString());
                    throw;
                }
            }
            return userId;
        } 
        public static string GetCurrentUserRole(HttpContext context,ILogger logger)
        {
            if (context is null)
            {
                throw new ArgumentException(nameof(context));
            }

            if (logger is null)
            {
                throw new ArgumentException(nameof(logger));
            }

            string role = "";
            if (context.User.Identity is ClaimsIdentity identity)
            {
                try
                {
                    role = identity.FindFirst("Role")!.Value;
                }
                catch (ArgumentException ex)
                {
                    TMSLogger.GeneralException(ex,logger,nameof(GetCurrentUserId));
                    logger.LogInformation(ex.ToString());
                    throw;
                }
            }
            return role;
        } 
    }
}