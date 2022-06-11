using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles(AppDbContext dbContext);
        IEnumerable<User> GetUsersByRole(int roleId, AppDbContext dbContext);
    }
}

