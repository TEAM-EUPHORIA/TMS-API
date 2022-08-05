using TMS.API.UtilityFunctions;

namespace TMS.API.Services
{

    public partial class UserService : IUserService
    {
        readonly Dictionary<string, string> DashboardResult = new();
        /// <summary>
        /// used to get dashboard data for the user by using user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// result Dictionary if user is found 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> Dashboard(int userId)
        {
            try
            {
                var userExists = _repo.Validation.UserExists(userId);
                var user = _repo.Users.GetUserById(userId);
                if (userExists)
                {
                    switch (user.RoleId)
                    {
                        case 1:
                            PrepareHeadDashboard(userId);
                            break;
                        case 2:
                            PrepareCoordinatorDashboard(userId);
                            break;
                        case 3:
                            PrepareTrainerDashboard(userId);
                            break;
                        case 4:
                            PrepareTraineeDashboard(userId);
                            break;
                        case 5:
                            PrepareReviewerDashboard(userId);
                            break;
                    }
                    return DashboardResult!;
                }
                else throw new ArgumentException("Invalid User");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(Dashboard));
                throw;
            }
             catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(Dashboard));
                throw;
            }
        }
    }
}