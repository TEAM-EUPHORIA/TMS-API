using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor of RoleService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public RoleService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            return _repo.Roles.GetRoles();
        }
    }
}
