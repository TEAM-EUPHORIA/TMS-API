using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<User> GetUsersByRole(int roleId);
    }
}

