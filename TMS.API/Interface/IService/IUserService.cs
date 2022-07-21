using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API.Services
{
    public interface IUserService
    {
        Dictionary<string, string> CreateUser(User user, int createdBy);
        Dictionary<string, string> Dashboard(int userId);
        bool DisableUser(int userId, int updatedBy);
        User GetUser(int userId);
        IEnumerable<User> GetUsersByDepartment(int departmentId);
        IEnumerable<User> GetUsersByDeptandRole(int departmentId, int roleId);
        IEnumerable<User> GetUsersByRole(int roleId);
        Dictionary<string, string> UpdateUser(UpdateUserModel user, int updatedBy);
    }
}