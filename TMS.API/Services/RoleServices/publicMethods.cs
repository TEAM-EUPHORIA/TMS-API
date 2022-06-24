using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
    }

    public class RoleService : IRoleService
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
    }
}

