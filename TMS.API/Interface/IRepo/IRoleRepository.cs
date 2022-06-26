using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
    }
}

