using TMS.BAL;

namespace TMS.API.Services
{
    public interface IUserService
    {
        Dictionary<string, string> CreateUser(User user);
        Dictionary<string, string> Dashboard(int userId);
        bool DisableUser(int userId, int currentUserId);
        User GetUserById(int userId);
        IEnumerable<User> GetUsersByDepartment(int departmentId);
        IEnumerable<User> GetUsersByDeptandrole(int departmentId, int roleId);
        IEnumerable<User> GetUsersByRole(int roleId);
        Dictionary<string, string> UpdateUser(User user);
    }

    public partial class UserService : IUserService
    {
        public Dictionary<string, string> Dashboard(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            var user = _repo.Users.GetUserById(userId);
            var result = new Dictionary<string, string>();
            if (userExists)
            {
                switch (user.RoleId)
                {
                    case 1:
                        result = prepareHeadDashboard(userId);
                        break;
                    case 2:
                        result = prepareCoordinatorDashboard(userId);
                        break;
                    case 3:
                        result = prepareTrainerDashboard(userId);
                        break;
                    case 4:
                        result = prepareTraineeDashboard(userId);
                        break;
                    case 5:
                        result = prepareReviewerDashboard(userId);
                        break;
                }
                return result;
            }
            throw new ArgumentException("Invalid User");
        }
    }
}