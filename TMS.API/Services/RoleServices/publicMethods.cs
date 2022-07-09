using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger<RoleService> _logger;
        /// <summary>
        /// Constructor of RoleService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public RoleService(IUnitOfWork repo,ILogger<RoleService> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        /// <summary>
        /// used to get roles
        /// </summary>
        /// <returns>
        /// IEnumerable role
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _repo.Roles.GetRoles();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(RoleService),nameof(GetRoles));
                throw;
            }
        }
    }
}

