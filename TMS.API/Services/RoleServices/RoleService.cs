using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class RoleService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RoleService> _logger;
        public RoleService(AppDbContext context, ILogger<RoleService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
       
        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _context.Roles.ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(RoleService), nameof(GetRoles));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(RoleService), nameof(GetRoles));
                throw;
            }
        }


    }
}

