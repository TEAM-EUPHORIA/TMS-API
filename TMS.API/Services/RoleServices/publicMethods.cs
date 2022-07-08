using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _repo;


        public RoleService(IUnitOfWork repo)
        {
            _repo = repo;

        }
        public IEnumerable<Role> GetRoles()
        {
            return _repo.Roles.GetRoles();
        }
    }
}

