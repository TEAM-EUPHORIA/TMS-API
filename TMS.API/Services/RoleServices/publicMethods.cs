using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public class RoleService
    {
        private readonly UnitOfWork _repo;
        

        public RoleService(UnitOfWork repo)
        {
            _repo = repo;
            
        }
        public IEnumerable<Role> GetRoles()
        {
            return _repo.Roles.GetRoles();
        }
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            var roleExists = _repo.Validation.RoleExists(roleId);
            if (roleExists) return _repo.Roles.GetUsersByRole(roleId);
            else throw new ArgumentException("Invalid Id");
        }
    }
}

