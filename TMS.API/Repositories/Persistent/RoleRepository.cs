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
    }
}