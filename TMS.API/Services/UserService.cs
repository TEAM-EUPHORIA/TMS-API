using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            if (roleId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUsersByRole));
            try
            {
                return _context.Users.Where(u => u.RoleId == roleId).Include("Role").Include("Department").ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUsersByRole));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUsersByRole));
                throw;
            }
        }
        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            if (departmentId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUsersByDepartment));
            try
            {
                return _context.Users.Where(u => (u.DepartmentId != 0 && u.DepartmentId == departmentId)).ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUsersByDepartment));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUsersByDepartment));
                throw;
            }
        }
        public User GetUserById(int id)
        {
            if (id == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUserById));
            try
            {
                var dbUser = _context.Users.Find(id);
                if (dbUser != null)
                {
                    string base64String = Convert.ToBase64String(dbUser.Image, 0, dbUser.Image.Length);
                    User result = new User();
                    result.Base64 = result.Base64 + base64String;
                    if (dbUser.DepartmentId != null)
                    {
                        result = _context.Users.Where(u => u.Id == id).Include("Role").Include("Department").FirstOrDefault();
                        return result;
                    }
                    else
                    {
                        result = _context.Users.Where(u => u.Id == id).Include("Role").FirstOrDefault();
                        return result;
                    }
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUserById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUserById));
                throw;
            }
        }
        public bool CreateUser(User user)
        {
            if (user == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateUser), nameof(user));
            try
            {
                string Imagedate = "";
                var newUserEmployeeId = _context.Users.ToList().Count() + 1;

                user.EmployeeId = ($"ACE{user.RoleId}{newUserEmployeeId}");

                user.Password = HashPassword.Sha256(user.Password);

                Imagedate = ImageService.Getbase64String(user.Base64);
                user.Base64 = ImageService.Getbase64Header(user.Base64);
                user.Image = System.Convert.FromBase64String(Imagedate);

                user.isDisabled = false;
                user.CreatedOn = DateTime.Now;

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(CreateUser));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(CreateUser));
                throw;
            }

        }

        public bool UpdateUser(User user)
        {
            if (user == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateUser), nameof(user));
            try
            {
                var dbUser = _context.Users.Find(user.Id);
                if (dbUser != null)
                {
                    string Imagedate = "";
                    dbUser.EmployeeId = user.EmployeeId;

                    dbUser.Password = HashPassword.Sha256(user.Password);

                    Imagedate = ImageService.Getbase64String(user.Base64);
                    dbUser.Base64 = ImageService.Getbase64Header(user.Base64);
                    dbUser.Image = System.Convert.FromBase64String(Imagedate);

                    dbUser.isDisabled = false;
                    dbUser.UpdatedOn = DateTime.Now;

                    _context.Update(dbUser);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(UpdateUser));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(UpdateUser));
                throw;
            }
        }



        public bool DisableUser(int userId)
        {
            if (userId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableUser));
            try
            {
                var dbUser = _context.Users.Find(userId);
                if (dbUser != null)
                {
                    dbUser.isDisabled = true;
                    dbUser.UpdatedOn = DateTime.Now;
                    _context.Update(dbUser);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(DisableUser));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(DisableUser));
                throw;
            }
        }
    }
}

