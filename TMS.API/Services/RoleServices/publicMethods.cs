using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public class RoleService
    {
        public IEnumerable<Role> GetRoles(AppDbContext dbContext)
        {
            return dbContext.Roles.ToList();
        }
        public IEnumerable<User> GetUsersByRole(int roleId, AppDbContext dbContext)
        {
            var roleExists = Validation.RoleExists(dbContext, roleId);
            if (roleExists) return dbContext.Users.Where(u => u.RoleId == roleId).Include(u => u.Role).Include(u => u.Department!).ToList();
            else throw new ArgumentException("Invalid Id");
        }
    }
}

