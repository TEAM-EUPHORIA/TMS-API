using TMS.BAL;

namespace TMS.API.Repositories
{
    class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Role> GetRoles()
        {
            return dbContext.Roles.ToList();
        }

        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            return dbContext.Users.Where(u=>u.RoleId == roleId);
        }
    }
}