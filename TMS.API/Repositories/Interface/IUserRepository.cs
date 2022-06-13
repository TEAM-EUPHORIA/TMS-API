using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersByRole(int roleId);
        IEnumerable<User> GetUsersByDepartment(int departmentId);
        IEnumerable<User> GetUsersByDeptandrole(int departmentId, int roleId);
        User GetUserByEmailAndPassword(LoginModel user);
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
    }
}