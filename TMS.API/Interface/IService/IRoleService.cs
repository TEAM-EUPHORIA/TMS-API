using TMS.BAL;

namespace TMS.API.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
    }
}

