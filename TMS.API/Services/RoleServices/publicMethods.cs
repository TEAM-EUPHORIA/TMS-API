using TMS.BAL;

namespace TMS.API.Services
{
    public class RoleService
    {
        public IEnumerable<Role> GetRoles(AppDbContext dbContext)
        {
            return dbContext.Roles.ToList();
        }
    }
}

